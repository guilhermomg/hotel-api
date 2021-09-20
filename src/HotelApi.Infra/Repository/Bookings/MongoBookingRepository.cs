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

        public Task<bool> Delete(Guid uid)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetByUid(Guid uid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Booking>> IntersectsInInterval(DateTime from, DateTime to, Guid? ignoreId = null)
        {
            return await _context.BookingsCollection.AsQueryable().Where(b =>
                (from <= b.DateTo && to >= b.DateFrom) &&
                        (ignoreId == null || (ignoreId != null && b.Uid != ignoreId))
            ).ToListAsync();
        }

        public Task<Booking> Update(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}