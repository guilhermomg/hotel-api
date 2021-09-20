using FluentValidation;

namespace HotelApi.Domain.Entities.Clients.Rules
{
    public class ClientValidation : AbstractValidator<Client>
    {
        public ClientValidation()
        {
            RuleFor(c => c.Ssn)
                .NotNull().NotEmpty()
                .WithMessage("Ssn field is required");

            RuleFor(c => c.FirstName)
                .NotNull().NotEmpty()
                .WithMessage("FirstName field is required");

            RuleFor(c => c.LastName)
                .NotNull().NotEmpty()
                .WithMessage("LastName field is required");

            RuleFor(c => c.DateOfBirth)
                .NotNull().NotEmpty()
                .WithMessage("DateOfBirth field is required");

            RuleFor(c => c.Email)
                .NotNull().NotEmpty()
                .WithMessage("Email field is required")
                .EmailAddress()
                .WithMessage("Email is invalid");
        }
    }
}