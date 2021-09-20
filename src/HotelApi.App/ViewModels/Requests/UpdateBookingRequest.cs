using System;
using HotelApi.App.ViewModels.Requests;

namespace HotelApi.Api.ViewModels.Requests
{
    public class UpdateBookingRequest
    {
        public Guid Uid { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public ClientRequest Client { get; set; }
    }
}