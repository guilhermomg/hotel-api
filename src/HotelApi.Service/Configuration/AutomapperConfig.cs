using AutoMapper;
using HotelApi.Domain.Entities.Bookings;
using HotelApi.Service.Bookings.Dto;

namespace HotelApi.Service.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<BookingDto, Booking>()
                .ForMember(dest => dest.DateFrom, o => o.MapFrom(src => src.DateFrom.Date))
                .ForMember(dest => dest.DateTo, o => o.MapFrom(src => src.DateTo.Date));
        }
    }
}