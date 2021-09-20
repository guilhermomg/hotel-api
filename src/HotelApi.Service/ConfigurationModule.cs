using HotelApi.Service.Bookings.Interfaces;
using HotelApi.Service.Bookings.Services;
using HotelApi.Service.Bookings.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HotelApi.Service
{
    public static class ConfigurationModule
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<ICheckAvailabilityService, CheckAvailabilityService>();
            services.AddScoped<IGetBookingByUidService, GetBookingByUidService>();
            services.AddScoped<IDeleteBookingService, DeleteBookingService>();
            services.AddScoped<ICreateBookingService, CreateBookingService>();
            services.AddScoped<IUpdateBookingService, UpdateBookingService>();
        }
    }
}