using System;
using HotelApi.Domain.Entities.Clients;

namespace HotelApi.Service.Bookings.Dto
{
    public class BookingDto
    {
        public BookingDto()
        {
            this.Client = new Client();
        }

        public Guid Uid { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Client Client { get; set; }
    }
}