using System;
using HotelApi.App.ViewModels.Requests;

namespace HotelApi.Api.ViewModels.Requests
{
    public class CreateBookingRequest
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public ClientRequest Client { get; set; }
    }
}