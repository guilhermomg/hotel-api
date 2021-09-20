using Bogus;
using Bogus.Extensions.Canada;
using HotelApi.Domain.Entities.Clients;

namespace HotelApi.Tests.Fixtures
{
    public class ClientFixture
    {
        public Client GetValidClient()
        {
            Person p = new();

            var fakeClient = new Faker<Client>()
                .RuleFor(b => b.Ssn, f => p.Sin())
                .RuleFor(b => b.FirstName, f => p.FirstName)
                .RuleFor(b => b.LastName, f => p.LastName)
                .RuleFor(b => b.DateOfBirth, f => p.DateOfBirth)
                .RuleFor(b => b.Email, f => p.Email);

            return fakeClient.Generate();
        }
    }
}