using System;
using AutoMapper;
using System.Threading.Tasks;
using HotelApi.Api.ViewModels.Requests;
using HotelApi.Service.Bookings.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelApi.App.Controllers;
using HotelApi.Domain.Entities.Notifications.Interfaces;
using HotelApi.Service.Bookings.Services.Interfaces;
using HotelApi.Service.Bookings.Dto;
using HotelApi.Domain.Entities.Bookings;
using HotelApi.App.ViewModels.Requests;

namespace HotelApi.Api.Controllers
{
    public class BookingController : BaseController
    {
        private readonly ICheckAvailabilityService _checkAvailabilityService;
        private readonly IGetBookingByUidService _getBookingByUidService;
        private readonly ICreateBookingService _createBookingService;
        private readonly IUpdateBookingService _updateBookingService;
        private readonly IDeleteBookingService _deleteBookingService;
        private readonly IMapper _mapper;

        public BookingController(
            ICheckAvailabilityService checkAvailabilityService,
            IGetBookingByUidService getBookingByUidService,
            ICreateBookingService createBookingService,
            IUpdateBookingService updateBookingService,
            IDeleteBookingService deleteBookingService,
            IMapper mapper,
            INotifier notifier
        ) : base(notifier)
        {
            _checkAvailabilityService = checkAvailabilityService;
            _getBookingByUidService = getBookingByUidService;
            _createBookingService = createBookingService;
            _updateBookingService = updateBookingService;
            _deleteBookingService = deleteBookingService;
            _mapper = mapper;
        }

        [HttpGet("availability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DaysAvailabilityDto>> CheckAvailability([FromQuery] CheckAvailabilityRequest request) =>
            CustomResponse(
                await _checkAvailabilityService.CheckAvailability(request.DateFrom, request.DateTo)
            );

        [HttpGet("{uid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Booking>> GetBookingByUid([FromRoute] Guid uid) =>
            CustomResponse(await _getBookingByUidService.Get(uid));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Booking>> Create(CreateBookingRequest request)
        {
            var dto = _mapper.Map<BookingDto>(request);

            var result = await _createBookingService.Create(dto);

            if (ValidOperation())
                return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
            else
                return CustomResponse();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Booking>> Update(UpdateBookingRequest request)
        {
            var dto = _mapper.Map<BookingDto>(request);

            var result = await _updateBookingService.Update(dto);

            return CustomResponse(result);
        }

        [HttpDelete("{uid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid uid)
        {
            await _deleteBookingService.Delete(uid);

            if (ValidOperation())
                return NoContent();
            else
                return CustomResponse();
        }
    }
}