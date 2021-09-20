using System;

namespace HotelApi.App.ViewModels.Requests
{
    public class CheckAvailabilityRequest
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}