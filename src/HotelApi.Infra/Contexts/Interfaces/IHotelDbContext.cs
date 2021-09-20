using HotelApi.Domain.Entities.Bookings;
using MongoDB.Driver;

namespace HotelApi.Infra.Repository.Contexts.Interfaces
{
    public interface IHotelDbContext
    {
        MongoClient Client { get; }
        IMongoCollection<Booking> BookingsCollection { get; }
    }
}