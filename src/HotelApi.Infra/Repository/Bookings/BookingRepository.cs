using System.Threading.Tasks;
using System.Collections.Generic;
using HotelApi.Infra.Repository.Bookings.Interfaces;
using HotelApi.Domain.Entities.Bookings;
using MongoDB.Bson;
using System;
using System.Linq;

namespace HotelApi.Infra.Repository.Bookings
{
    public class BookingRepository : IBookingRepository
    {
        private List<Booking> _list;

        public BookingRepository()
        {
            _list = new List<Booking>();
        }

        public async Task<Booking> Add(Booking booking)
        {
            _list.Add(booking);
            return await Task.FromResult(booking);
        }

        public async Task<Booking> Update(Booking booking)
        {
            var updatedBooking = _list.FirstOrDefault(b => b.Uid == booking.Uid);
            var index = _list.IndexOf(updatedBooking);

            updatedBooking.DateFrom = booking.DateFrom;
            updatedBooking.DateTo = booking.DateTo;
            updatedBooking.LastUpdateAt = booking.LastUpdateAt;

            _list[index] = updatedBooking;

            return await Task.FromResult(updatedBooking);
        }

        public async Task<Boolean> Delete(Guid uid)
        {
            var result = _list.RemoveAll(b => b.Uid == uid);
            return await Task.FromResult(result > 0);
        }

        public async Task<Booking> GetByUid(Guid uid)
        {
            return await Task.FromResult(
                _list.FirstOrDefault(b => b.Uid == uid)
            );
        }

        public async Task<IEnumerable<Booking>> IntersectsInInterval(DateTime from, DateTime to, Guid? ignoreId)
        {
            return await Task.FromResult(
                    _list.Where(b =>
                        (from <= b.DateTo && to >= b.DateFrom) &&
                        (ignoreId == null || (ignoreId != null && b.Uid != ignoreId))
                    ).ToList()
            );
        }
    }
}