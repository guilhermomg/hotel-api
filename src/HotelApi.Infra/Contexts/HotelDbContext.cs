using HotelApi.Domain.Entities.Bookings;
using HotelApi.Infra.Configuration;
using HotelApi.Infra.Repository.Contexts.Interfaces;
using MongoDB.Driver;

namespace HotelApi.Infra.Repository.Bookings.Contexts
{
    public class HotelDbContext : IHotelDbContext
    {
        public HotelDbContext(IMongoDatabaseSettings settings)
        {
            Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);

            BookingsCollection = database.GetCollection<Booking>(settings.BookingsCollectionName);
        }

        public MongoClient Client { get; }
        public IMongoCollection<Booking> BookingsCollection { get; }
    }
}