using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 2. Desenvolva o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {
            return (IEnumerable<CityDto>)_context.Cities.Select(city => new CityDto
            {
                CityId = city.CityId,
                Name = city.Name
            });
        }

        // 3. Desenvolva o endpoint POST /city
        public CityDto AddCity(City city)
        {
            _context.Cities.Add(city);

            _context.SaveChanges();

            return new CityDto
            {
                CityId = city.CityId, Name = city.Name
            };
        }

    }
}