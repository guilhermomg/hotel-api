using System;
using System.Threading.Tasks;
using AutoMapper;
using HotelApi.Domain.Entities.Notifications;
using HotelApi.Infra.Repository.Bookings;
using HotelApi.Service.Bookings.Interfaces;
using HotelApi.Service.Bookings.Services;
using HotelApi.Service.Bookings.Services.Interfaces;
using HotelApi.Tests.Fixtures;
using Moq;
using Xunit;

namespace HotelApi.Tests
{
    public class CheckAvailabilityTests
    {
        private readonly ICreateBookingService _createBookingService;
        private readonly ICheckAvailabilityService _checkAvailabilityService;
        private readonly BookingFixture _bookingFixture;

        public CheckAvailabilityTests()
        {
            var bookingRepository = new BookingRepository();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new App.Configuration.AutomapperConfig());
                cfg.AddProfile(new Service.Configuration.AutomapperConfig());
            });
            var mapper = mockMapper.CreateMapper();
            var notifier = new Notifier();

            _createBookingService = new CreateBookingService(bookingRepository,
                mapper, notifier);

            _checkAvailabilityService = new CheckAvailabilityService(
                bookingRepository,
                notifier
            );
            _bookingFixture = new BookingFixture();
        }

        [Fact]
        public async Task ShouldNotReturnResultsWithInvalidDates()
        {
            var dateFrom = DateTime.Today.AddDays(-1);
            var dateTo = dateFrom.AddDays(1);

            var result = await _checkAvailabilityService.CheckAvailability(dateFrom, dateTo);
            Assert.Null(result);

            dateFrom = DateTime.Today.AddDays(1);
            dateTo = DateTime.Today.AddDays(60);

            result = await _checkAvailabilityService.CheckAvailability(dateFrom, dateTo);
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldReturnResultsWithValidDates()
        {
            var booking = _bookingFixture.GetValidCreateDto();

            var result = await _createBookingService.Create(booking);
            Assert.True(result.Uid != Guid.Empty);

            var dateFrom = DateTime.Today.AddDays(1);
            var dateTo = DateTime.Today.AddDays(30);

            var availabilityResult = await _checkAvailabilityService.CheckAvailability(dateFrom, dateTo);
            Assert.NotNull(availabilityResult);
        }
    }
}