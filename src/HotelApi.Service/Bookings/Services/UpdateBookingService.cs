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
    public class UpdateBookingService : BaseService, IUpdateBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public UpdateBookingService(
            IBookingRepository bookingRepository,
            IMapper mapper,
            INotifier notifier
        ) : base(notifier)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<Booking> Update(BookingDto dto)
        {
            var originalBooking = await _bookingRepository.GetByUid(dto.Uid);

            if (originalBooking == null)
            {
                Notify($"Booking not found with unique identifier {dto.Uid}");
                return null;
            }

            var bookingList = await _bookingRepository.IntersectsInInterval(
                dto.DateFrom, dto.DateTo, dto.Uid);

            if (bookingList.Any())
            {
                Notify("Dates not available");
                return null;
            }

            var booking = _mapper.Map<Booking>(dto);

            if (booking.IsValid())
            {
                booking.LastUpdateAt = DateTime.Now;
                return await _bookingRepository.Update(booking);
            }
            else
            {
                Notify(booking.ValidationResult);
                return null;
            }
        }
    }
}