using System;
using HotelApi.Domain.Entities.Clients.Rules;

namespace HotelApi.Domain.Entities.Clients
{
    public class Client : Entity
    {
        public string Ssn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ClientValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}