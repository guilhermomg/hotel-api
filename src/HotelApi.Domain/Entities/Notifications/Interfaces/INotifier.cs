using System.Collections.Generic;

namespace HotelApi.Domain.Entities.Notifications.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
