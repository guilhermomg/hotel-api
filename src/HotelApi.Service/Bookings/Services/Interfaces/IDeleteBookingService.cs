using System;
using System.Threading.Tasks;

namespace HotelApi.Service.Bookings.Services.Interfaces
{
    public interface IDeleteBookingService
    {
        Task Delete(Guid uid);
    }
}