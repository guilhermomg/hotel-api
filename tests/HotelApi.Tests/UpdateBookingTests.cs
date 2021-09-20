using System;
using System.Threading.Tasks;
using AutoMapper;
using HotelApi.Domain.Entities.Notifications;
using HotelApi.Infra.Repository.Bookings;
using HotelApi.Service.Bookings.Services;
using HotelApi.Service.Bookings.Services.Interfaces;
using HotelApi.Tests.Fixtures;
using Xunit;

namespace HotelApi.Tests
{
    public class UpdateBookingTests
    {
        private readonly ICreateBookingService _createBookingService;
        private readonly IUpdateBookingService _updateBookingService;

        private readonly BookingFixture _bookingFixture;

        public UpdateBookingTests()
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

            _updateBookingService = new UpdateBookingService(bookingRepository,
                mapper, notifier);

            _bookingFixture = new BookingFixture();
        }

        [Fact]
        public async Task ShouldNotUpdateNonExistingBooking()
        {
            var booking = _bookingFixture.GetInvalidUpdateDto_NonExistingUid();

            var result = await _updateBookingService.Update(booking);
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldNotUpdateBookingToUnavailableDates()
        {
            var booking = _bookingFixture.GetValidCreateDto();

            var createResult = await _createBookingService.Create(booking);
            Assert.True(createResult.Uid != Guid.Empty);

            var firstBookingUid = createResult.Uid;

            booking.DateFrom = booking.DateTo.AddDays(1);
            booking.DateTo = booking.DateFrom.AddDays(2);

            createResult = await _createBookingService.Create(booking);
            Assert.True(createResult.Uid != Guid.Empty);

            booking.Uid = firstBookingUid;
            var updateResult = await _updateBookingService.Update(booking);
            Assert.Null(updateResult);
        }

        [Fact]
        public async Task ShouldNotUpdateBookingToInvalidDates()
        {
            var booking = _bookingFixture.GetValidCreateDto();

            var createResult = await _createBookingService.Create(booking);
            Assert.True(createResult.Uid != Guid.Empty);

            var firstBookingUid = createResult.Uid;

            booking = _bookingFixture.GetInvalidCreateDto_InvalidStayPeriod();
            booking.Uid = firstBookingUid;

            var updateResult = await _updateBookingService.Update(booking);
            Assert.Null(updateResult);
        }

        [Fact]
        public async Task ShouldUpdateBookingToNewDates()
        {
            var booking = _bookingFixture.GetValidCreateDto();

            var createResult = await _createBookingService.Create(booking);
            Assert.True(createResult.Uid != Guid.Empty);

            booking.Uid = createResult.Uid;
            booking.DateFrom = createResult.DateFrom.AddDays(1);
            booking.DateTo = createResult.DateTo.AddDays(1);

            var updateResult = await _updateBookingService.Update(booking);
            Assert.NotNull(updateResult);
        }
    }
}