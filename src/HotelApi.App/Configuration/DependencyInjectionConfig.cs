using HotelApi.Domain.Entities.Notifications;
using HotelApi.Domain.Entities.Notifications.Interfaces;
using HotelApi.Infra;
using HotelApi.Infra.Repository.Bookings.Contexts;
using HotelApi.Infra.Repository.Contexts.Interfaces;
using HotelApi.Service;
using Microsoft.Extensions.DependencyInjection;

namespace HotelApi.App.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IHotelDbContext, HotelDbContext>();

            services.RegisterService();
            services.RegisterRepository();

            return services;
        }

    }
}