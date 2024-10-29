using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("hotel")]
    public class HotelController : Controller
    {
        private readonly IHotelRepository _repository;

        public HotelController(IHotelRepository repository)
        {
            _repository = repository;
        }
        
        // 4. Desenvolva o endpoint GET /hotel
        [HttpGet]
        public IActionResult GetHotels(){
            try
            {
                var hotels = _repository.GetHotels();
                if (hotels == null || hotels.Count() == 0)
                {
                    return Ok(new List<HotelDto>());
                }

                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        // 5. Desenvolva o endpoint POST /hotel
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Admin")]
        public IActionResult PostHotel([FromBody] Hotel hotel){
            try
            {
                return Created("", _repository.AddHotel(hotel));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }


    }
}