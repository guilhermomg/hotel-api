using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelApi.Domain.Entities.Bookings;
using HotelApi.Domain.Entities.Notifications.Interfaces;
using HotelApi.Infra.Repository.Bookings.Interfaces;
using HotelApi.Service.Bookings.Dto;
using HotelApi.Service.Bookings.Services.Interfaces;

namespace HotelApi.Service.Bookings.Services
{
    public class CreateBookingService : BaseService, ICreateBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public CreateBookingService(
            IBookingRepository bookingRepository,
            IMapper mapper,
            INotifier notifier
        ) : base(notifier)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<Booking> Create(BookingDto dto)
        {
            var bookingList = await _bookingRepository.IntersectsInInterval(dto.DateFrom, dto.DateTo);

            if (bookingList.Any())
            {
                Notify("Dates not available");
                return null;
            }

            var booking = _mapper.Map<Booking>(dto);

            if (booking.IsValid())
            {
                booking.Uid = Guid.NewGuid();
                booking.CreatedAt = DateTime.Now;
                return await _bookingRepository.Add(booking);
            }
            else
            {
                Notify(booking.ValidationResult);
                if (booking.Client.ValidationResult != null)
                    Notify(booking.Client.ValidationResult);
                return null;
            }
        }
    }
}