using System.Threading.Tasks;
using HotelApi.Domain.Entities.Bookings;
using HotelApi.Service.Bookings.Dto;

namespace HotelApi.Service.Bookings.Services.Interfaces
{
    public interface IUpdateBookingService
    {
        Task<Booking> Update(BookingDto dto);
    }
}