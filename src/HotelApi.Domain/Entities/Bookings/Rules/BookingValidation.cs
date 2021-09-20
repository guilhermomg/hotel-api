using System;
using FluentValidation;
using HotelApi.Domain.Entities.Bookings;

namespace HotelApi.Domain.Entities.Bookings.Rules
{
    public class BookingValidation : AbstractValidator<Booking>
    {
        public BookingValidation()
        {
            RuleFor(b => b.DateFrom)
                .GreaterThanOrEqualTo(DateTime.Today.AddDays(1))
                .WithMessage($"Current minimum booking date is {DateTime.Today.AddDays(1).ToString("MM/dd/yyyy")}");

            RuleFor(b => b.DateFrom)
                .LessThan(b => b.DateTo)
                .WithMessage("DateFrom must be before DateTo");

            RuleFor(b => (b.DateTo - b.DateFrom).TotalDays)
                .LessThan(3)
                .WithMessage("The stay can't be longer than 3 days");

            RuleFor(b => (b.DateFrom - DateTime.Today).TotalDays)
                .LessThanOrEqualTo(30)
                .WithMessage("Bookings can't be reserved more than 30 days in advance");

            RuleFor(b => b.Client)
                .NotNull()
                .WithMessage("Client informations must be provided");
        }
    }
}