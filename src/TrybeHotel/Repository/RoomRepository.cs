using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 6. Desenvolva o endpoint GET /room/:hotelId
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            var rooms = _context.Rooms.Include(r => r.Hotel)
                .Where(r => r.HotelId == HotelId)
                .Select(room => new RoomDto
                {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto
                    {
                         HotelId = room.Hotel.HotelId,
                         Name = room.Hotel.Name,
                         Address = room.Hotel.Address,
                         CityId = room.Hotel.CityId,
                         CityName = room.Hotel.City.Name
                    }
            });

            return rooms;
        }

        // 7. Desenvolva o endpoint POST /room
        public RoomDto AddRoom(Room room) {
            var hotel = _context.Hotels
                .Include(h => h.City)
                .FirstOrDefault(h => h.HotelId == room.HotelId);

            _context.Rooms.Add(room) ;
            _context.SaveChanges();

            return new RoomDto
            {
                RoomId = room.RoomId,
                Name = room.Name,
                Capacity = room.Capacity,
                Image = room.Image,
                Hotel = new HotelDto
                {
                    HotelId = hotel.HotelId,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    CityId = hotel.CityId,
                    CityName = hotel.City.Name
                }
            };
        }

        // 8. Desenvolva o endpoint DELETE /room/:roomId
        public void DeleteRoom(int RoomId) {
            var room = _context.Rooms.Find(RoomId);
            
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }
    }
}