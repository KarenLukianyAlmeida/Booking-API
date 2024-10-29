using TrybeHotel.Models;
using TrybeHotel.Dto;
using System.Runtime.Serialization;
using TrybeHotel.Exceptions;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
           throw new NotImplementedException();
        }
        public UserDto Add(UserDtoInsert user)
        {
            var existsEmail = _context.Users.Any(u => u.Email == user.Email);

            if (existsEmail)
            {
                throw new ConflictException();
            }

            var newUser = new User { 
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
             };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return new UserDto
            {
                UserId = newUser.UserId,
                Name = user.Name,
                Email = user.Email,
                UserType = newUser.UserType
            };
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetUsers()
        {
           throw new NotImplementedException();
        }

    }


}