using HotelApi.Infra.Repository.Bookings;
using HotelApi.Infra.Repository.Bookings.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HotelApi.Infra
{
    public static class ConfigurationModule
    {
        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddSingleton<IBookingRepository, BookingRepository>();
            //services.AddScoped<IBookingRepository, MongoBookingRepository>();
        }
    }
}