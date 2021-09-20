using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using HotelApi.Service.Bookings.Dto;

namespace HotelApi.Service.Bookings.Interfaces
{
    public interface ICheckAvailabilityService
    {
        Task<IEnumerable<DaysAvailabilityDto>> CheckAvailability(DateTime dateFrom, DateTime dateTo);
    }
}