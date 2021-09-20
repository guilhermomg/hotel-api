using System;
using System.Threading.Tasks;
using HotelApi.Domain.Entities.Bookings;
using HotelApi.Domain.Entities.Notifications.Interfaces;
using HotelApi.Infra.Repository.Bookings.Interfaces;
using HotelApi.Service.Bookings.Services.Interfaces;

namespace HotelApi.Service.Bookings.Services
{
    public class GetBookingByUidService : BaseService, IGetBookingByUidService
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingByUidService(
            IBookingRepository bookingRepository,
            INotifier notifier
        ) : base(notifier)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> Get(Guid uid)
        {
            var booking = await _bookingRepository.GetByUid(uid);

            if (booking == null)
            {
                Notify($"No booking was found with uid {uid.ToString()}");
                return null;
            }

            return booking;
        }
    }
}