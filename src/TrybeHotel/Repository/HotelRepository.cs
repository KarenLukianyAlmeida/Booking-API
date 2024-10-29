using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Desenvolva o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {
            return _context.Hotels.Select(hotel => new HotelDto
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Address = hotel.Address,
                CityId = hotel.CityId,
                CityName = hotel.City.Name
            });
        }
        
        // 5. Desenvolva o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);

            _context.SaveChanges();

            var city = _context.Cities.First(c => c.CityId == hotel.CityId);

            if (city == null)
            {
                throw new Exception("Cidade não encontrada");
            }

            return new HotelDto
            {
                HotelId = hotel.HotelId,
                Address = hotel.Address,
                CityId = hotel.CityId,
                CityName = city.Name,
                Name = hotel.Name
            };
        }
    }
}