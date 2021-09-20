using AutoMapper;
using HotelApi.Api.ViewModels.Requests;
using HotelApi.App.ViewModels.Requests;
using HotelApi.Domain.Entities.Clients;
using HotelApi.Service.Bookings.Dto;

namespace HotelApi.App.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<ClientRequest, Client>();

            CreateMap<CreateBookingRequest, BookingDto>()
                .ForMember(dest => dest.DateFrom, o => o.MapFrom(src => src.DateFrom.Date))
                .ForMember(dest => dest.DateTo, o => o.MapFrom(src => src.DateTo.Date));

            CreateMap<UpdateBookingRequest, BookingDto>()
                .ForMember(dest => dest.Uid, o => o.MapFrom(src => src.Uid))
                .ForMember(dest => dest.DateFrom, o => o.MapFrom(src => src.DateFrom.Date))
                .ForMember(dest => dest.DateTo, o => o.MapFrom(src => src.DateTo.Date))
                .ForMember(dest => dest.Client, o => o.MapFrom(src => src.Client));
        }
    }
}