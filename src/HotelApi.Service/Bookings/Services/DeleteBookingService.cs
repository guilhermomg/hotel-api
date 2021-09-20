using System;
using System.Threading.Tasks;
using HotelApi.Domain.Entities.Notifications.Interfaces;
using HotelApi.Infra.Repository.Bookings.Interfaces;
using HotelApi.Service.Bookings.Services.Interfaces;

namespace HotelApi.Service.Bookings.Services
{
    public class DeleteBookingService : BaseService, IDeleteBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public DeleteBookingService(
            IBookingRepository bookingRepository,
            INotifier notifier
        ) : base(notifier)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task Delete(Guid uid)
        {
            var originalBooking = await _bookingRepository.GetByUid(uid);

            if (originalBooking == null)
            {
                Notify($"Booking not found with unique identifier {uid}");
                return;
            }

            var result = await _bookingRepository.Delete(uid);

            if (!result)
                Notify("No bookings were deleted");
        }
    }
}