using System;
using System.Threading.Tasks;
using HotelApi.Domain.Entities.Bookings;

namespace HotelApi.Service.Bookings.Services.Interfaces
{
    public interface IGetBookingByUidService
    {
        Task<Booking> Get(Guid uid);
    }
}