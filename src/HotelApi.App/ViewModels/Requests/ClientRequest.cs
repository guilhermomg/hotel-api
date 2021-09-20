using System;

namespace HotelApi.App.ViewModels.Requests
{
    public class ClientRequest
    {
        public string Ssn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
    }
}