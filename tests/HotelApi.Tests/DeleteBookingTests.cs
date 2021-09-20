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
    public class DeleteBookingTests
    {
        private readonly ICreateBookingService _createBookingService;
        private readonly IDeleteBookingService _deleteBookingService;
        private readonly IGetBookingByUidService _getBookingByUidService;
        private readonly BookingFixture _bookingFixture;

        public DeleteBookingTests()
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

            _deleteBookingService = new DeleteBookingService(bookingRepository,
                mockNotifier.Object);

            _getBookingByUidService = new GetBookingByUidService(bookingRepository,
                mockNotifier.Object);

            _bookingFixture = new BookingFixture();
        }

        [Fact]
        public async Task ShouldDeleteExistingBooking()
        {
            var booking = _bookingFixture.GetValidCreateDto();

            var createResult = await _createBookingService.Create(booking);
            Assert.True(createResult.Uid != Guid.Empty);

            await _deleteBookingService.Delete(createResult.Uid);

            var deletedBooking = await _getBookingByUidService.Get(createResult.Uid);
            Assert.Null(deletedBooking);
        }

        [Fact]
        public async Task ShouldNotDeleteNonExistingBooking()
        {
            var booking = _bookingFixture.GetValidCreateDto();

            var createResult = await _createBookingService.Create(booking);
            Assert.True(createResult.Uid != Guid.Empty);

            await _deleteBookingService.Delete(Guid.NewGuid());

            var notDeletedBooking = await _getBookingByUidService.Get(createResult.Uid);
            Assert.NotNull(notDeletedBooking);
        }
    }
}
