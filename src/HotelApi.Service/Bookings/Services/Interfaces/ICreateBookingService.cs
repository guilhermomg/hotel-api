using HotelApi.Domain.Entities.Bookings;
using HotelApi.Service.Bookings.Dto;
using System.Threading.Tasks;

namespace HotelApi.Service.Bookings.Services.Interfaces
{
    public interface ICreateBookingService
    {
        Task<Booking> Create(BookingDto dto);
    }
}