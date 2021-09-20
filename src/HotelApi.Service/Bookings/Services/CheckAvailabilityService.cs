using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using HotelApi.Domain.Entities.Notifications.Interfaces;
using HotelApi.Infra.Repository.Bookings.Interfaces;
using HotelApi.Service.Bookings.Dto;
using HotelApi.Service.Bookings.Interfaces;

namespace HotelApi.Service.Bookings.Services
{
    public class CheckAvailabilityService : BaseService, ICheckAvailabilityService
    {
        private readonly IBookingRepository _bookingRepository;

        public CheckAvailabilityService(
            IBookingRepository bookingRepository,
            INotifier notifier
        ) : base(notifier)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<DaysAvailabilityDto>> CheckAvailability(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom <= DateTime.Today)
                Notify("DateFrom can only be greater than today");

            if (dateTo > DateTime.Today.AddDays(30))
                Notify("Maximum DateTo date is 30 days from today");

            if (!ValidOperation())
                return null;

            var bookingList = await _bookingRepository.IntersectsInInterval(
                dateFrom, dateTo);

            var result = new List<DaysAvailabilityDto>();

            for (var auxDate = dateFrom; auxDate <= dateTo; auxDate = auxDate.AddDays(1))
            {
                var available = !bookingList.Any(b =>
                    (auxDate <= b.DateTo && auxDate >= b.DateFrom)
                );

                result.Add(new DaysAvailabilityDto()
                {
                    Date = auxDate,
                    Available = available
                });
            }

            return result;
        }
    }
}