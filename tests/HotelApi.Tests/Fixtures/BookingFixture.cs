using System;
using Bogus;
using HotelApi.Service.Bookings.Dto;

namespace HotelApi.Tests.Fixtures
{
    public class BookingFixture
    {
        private ClientFixture _clientFixture;

        public BookingFixture()
        {
            _clientFixture = new ClientFixture();
        }


        public BookingDto GetValidCreateDto()
        {
            Bogus.DataSets.Date bogusDate = new();
            var fromDate = bogusDate.Soon(0, DateTime.Today.AddDays(1)).Date;

            var fakeBooking = new Faker<BookingDto>()
                .RuleFor(b => b.DateFrom, f => fromDate)
                .RuleFor(b => b.DateTo, f => fromDate.AddDays(f.Random.Int(1, 2)))
                .RuleFor(b => b.Client, f => _clientFixture.GetValidClient());

            return fakeBooking.Generate();
        }

        public BookingDto GetInvalidCreateDto_InvalidStayPeriod()
        {
            Bogus.DataSets.Date bogusDate = new();
            var fromDate = bogusDate.Soon().Date;

            var fakeBooking = new Faker<BookingDto>()
                .RuleFor(b => b.DateFrom, f => fromDate)
                .RuleFor(b => b.DateTo, f => fromDate.AddDays(f.Random.Int(3, 30)));

            return fakeBooking.Generate();
        }

        public BookingDto GetInvalidCreateDto_InvalidReserveInAdvance()
        {
            Bogus.DataSets.Date bogusDate = new();
            var fromDate = bogusDate.SoonOffset(0, DateTime.Today.AddDays(31)).Date;

            var fakeBooking = new Faker<BookingDto>()
                .RuleFor(b => b.DateFrom, f => fromDate)
                .RuleFor(b => b.DateTo, f => fromDate.AddDays(f.Random.Int(1, 2)));

            return fakeBooking.Generate();
        }

        public BookingDto GetInvalidCreateDto_ReserveStartToday()
        {
            DateTime fromDate = DateTime.Today;

            var fakeBooking = new Faker<BookingDto>()
                .RuleFor(b => b.DateFrom, f => fromDate)
                .RuleFor(b => b.DateTo, f => fromDate.AddDays(f.Random.Int(1, 2)));

            return fakeBooking.Generate();
        }

        public BookingDto GetInvalidUpdateDto_NonExistingUid()
        {
            Bogus.DataSets.Date bogusDate = new();
            var fromDate = bogusDate.Soon(0, DateTime.Today.AddDays(1)).Date;

            var fakeBooking = new Faker<BookingDto>()
                .RuleFor(b => b.Uid, f => Guid.NewGuid())
                .RuleFor(b => b.DateFrom, f => fromDate)
                .RuleFor(b => b.DateTo, f => fromDate.AddDays(f.Random.Int(1, 2)));

            return fakeBooking.Generate();
        }
    }
}