using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrybeHotel.Dto;
using System.Runtime.Serialization;
using TrybeHotel.Exceptions;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("booking")]
  
    public class BookingController : Controller
    {
        private readonly IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        [HttpPost]
        public IActionResult Add([FromBody] BookingDtoInsert bookingInsert){
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                BookingResponse newBooking = _repository.Add(bookingInsert, userEmail);
                
                return Created("", newBooking);
            }
            catch (CapacityExceededException)
            {
                return BadRequest(new { message = "Guest quantity over room capacity" });
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        [HttpGet("{Bookingid}")]
        public IActionResult GetBooking(int Bookingid){
           try
           {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                var booking = _repository.GetBooking(Bookingid, userEmail);

                return Ok(booking);
           }
           catch (Exception)
           {
                return Unauthorized();
           }
        }
    }
}