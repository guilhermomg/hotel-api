using System;

namespace HotelApi.Service.Bookings.Dto
{
    public class DaysAvailabilityDto
    {
        public DateTime Date { get; set; }
        public bool Available { get; set; }
    }
}