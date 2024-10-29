using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Dto;
using TrybeHotel.Models;
using TrybeHotel.Repository;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : Controller
    {
        private readonly ICityRepository _repository;
        public CityController(ICityRepository repository)
        {
            _repository = repository;
        }
        
        // 2. Desenvolva o endpoint GET /city
        [HttpGet]
        public IActionResult GetCities(){
            try
            {
                var cities = _repository.GetCities().ToList();
                
                if (cities == null || cities.Count() == 0)
                {
                    return Ok(new List<CityDto>());
                }

                return Ok(cities);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }

        // 3. Desenvolva o endpoint POST /city
        [HttpPost]
        public IActionResult PostCity([FromBody] City city){
            try
            {
                return Created("", _repository.AddCity(city));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message.ToString() });
            }
        }
    }
}