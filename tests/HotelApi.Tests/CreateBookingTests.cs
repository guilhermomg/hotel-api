using System;
using System.Threading.Tasks;
using AutoMapper;
using HotelApi.Domain.Entities.Notifications.Interfaces;
using HotelApi.Infra.Repository.Bookings;
using HotelApi.Service.Bookings.Services;
using HotelApi.Service.Bookings.Services.Interfaces;
using HotelApi.Tests.Fixtures;
using Moq;
using Xunit;

namespace HotelApi.Tests
{
    public class CreateBookingTests
    {
        private readonly ICreateBookingService _createBookingService;
        private readonly BookingFixture _bookingFixture;

        public CreateBookingTests()
        {
            var bookingRepository = new BookingRepository();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new App.Configuration.AutomapperConfig());
                cfg.AddProfile(new Service.Configuration.AutomapperConfig());
            });
            var mapper = mockMapper.CreateMapper();

            var mockNotifier = new Mock<INotifier>();

            _createBookingService = new CreateBookingService(bookingRepository,
                mapper, mockNotifier.Object);
            _bookingFixture = new BookingFixture();
        }

        [Fact]
        public async Task ShouldNotCreateInvalidBookings()
        {
            var booking = _bookingFixture.GetInvalidCreateDto_InvalidStayPeriod();

            var result = await _createBookingService.Create(booking);
            Assert.Null(result);

            booking = _bookingFixture.GetInvalidCreateDto_InvalidReserveInAdvance();

            result = await _createBookingService.Create(booking);
            Assert.Null(result);

            booking = _bookingFixture.GetInvalidCreateDto_ReserveStartToday();

            result = await _createBookingService.Create(booking);
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldCreateValidBooking()
        {
            var booking = _bookingFixture.GetValidCreateDto();

            var result = await _createBookingService.Create(booking);

            Assert.True(result.Uid != Guid.Empty);
        }

        [Fact]
        public async Task ShouldNotCreateBookingOnUnavailableDates()
        {
            var booking = _bookingFixture.GetValidCreateDto();

            var result = await _createBookingService.Create(booking);
            Assert.True(result.Uid != Guid.Empty);

            result = await _createBookingService.Create(booking);
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldNotCreateBookingWithInvalidClient()
        {
            var booking = _bookingFixture.GetValidCreateDto();
            booking.Client.Ssn = "";

            var result = await _createBookingService.Create(booking);
            Assert.Null(result);
        }
    }
}