using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelApi.Domain.Entities.Bookings;
using HotelApi.Infra.Repository.Bookings.Interfaces;
using HotelApi.Infra.Repository.Contexts.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HotelApi.Infra.Repository.Bookings
{
    public class MongoBookingRepository : IBookingRepository
    {
        private readonly IHotelDbContext _context;

        public MongoBookingRepository(IHotelDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Booking> Add(Booking booking)
        {
            await _context.BookingsCollection.InsertOneAsync(booking);
            return booking;
        }

        public async Task<bool> Delete(Guid uid)
        {
            var result = await _context.BookingsCollection.DeleteOneAsync(b => b.Uid == uid);
            return result.DeletedCount > 0;
        }

        public async Task<Booking> GetByUid(Guid uid)
        {
            return await _context.BookingsCollection.Find(b => b.Uid == uid).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Booking>> IntersectsInInterval(DateTime from, DateTime to, Guid? ignoreId = null)
        {
            return await _context.BookingsCollection.AsQueryable().Where(b =>
                (from <= b.DateTo && to >= b.DateFrom) &&
                        (ignoreId == null || (ignoreId != null && b.Uid != ignoreId))
            ).ToListAsync();
        }

        public async Task<Booking> Update(Booking booking)
        {
            var updateDefinition = Builders<Booking>.Update
                .Set(updateBooking => updateBooking.DateFrom, booking.DateFrom)
                .Set(updateBooking => updateBooking.DateTo, booking.DateTo)
                .Set(updateBooking => updateBooking.LastUpdateAt, DateTime.Now)
                .Set(updateBooking => updateBooking.Client, booking.Client);

            return await _context.BookingsCollection.FindOneAndUpdateAsync<Booking>(
                updateBooking => updateBooking.Uid == booking.Uid, updateDefinition
            );
        }
    }
}
