using System.Linq;
using HotelApi.Domain.Entities.Notifications;
using HotelApi.Domain.Entities.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.App.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }

        protected void NotifyError(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(new
                {
                    success = true,
                    data = result
                });

            return UnprocessableEntity(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }
    }
}