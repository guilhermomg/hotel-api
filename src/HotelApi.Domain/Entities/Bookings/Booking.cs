using System;
using HotelApi.Domain.Entities.Bookings.Rules;
using HotelApi.Domain.Entities.Clients;

namespace HotelApi.Domain.Entities.Bookings
{
    public class Booking : Entity
    {
        public Guid Uid { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdateAt { get; set; }
        public Client Client { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new BookingValidation().Validate(this);
            var clientIsValid = this.Client.IsValid();

            return ValidationResult.IsValid && clientIsValid;
        }
    }
}