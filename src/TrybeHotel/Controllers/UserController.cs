using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TrybeHotel.Exceptions;
using System.Security.Claims;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("user")]

    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public IActionResult GetUsers(){
            try
            {
                IEnumerable<UserDto> users = _repository.GetUsers();
                return Ok(users);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    
        [HttpPost]
        public IActionResult Add([FromBody] UserDtoInsert user)
        {
            try
            {
                return Created("", _repository.Add(user));
            }
            catch (ConflictException)
            {
                return Conflict(new { message = "User email already exists" });
            }
        }
    }
}