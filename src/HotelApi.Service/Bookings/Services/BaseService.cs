using FluentValidation.Results;
using HotelApi.Domain.Entities.Notifications;
using HotelApi.Domain.Entities.Notifications.Interfaces;

namespace HotelApi.Service.Bookings.Services
{
    public class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }
    }
}