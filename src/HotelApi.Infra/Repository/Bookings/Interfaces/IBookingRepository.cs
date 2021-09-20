using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelApi.Domain.Entities.Bookings;

namespace HotelApi.Infra.Repository.Bookings.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> Add(Booking booking);
        Task<Booking> Update(Booking booking);
        Task<Boolean> Delete(Guid uid);
        Task<Booking> GetByUid(Guid uid);
        Task<IEnumerable<Booking>> IntersectsInInterval(DateTime from, DateTime to, Guid? ignoreId = null);
    }
}