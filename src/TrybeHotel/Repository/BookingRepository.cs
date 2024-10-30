using TrybeHotel.Models;
using TrybeHotel.Dto;
using TrybeHotel.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            var room = _context.Rooms.Include(x => x.Hotel).ThenInclude(h => h.City).FirstOrDefault(r => r.RoomId == booking.RoomId);

            if (booking.GuestQuant > room.Capacity)
            {
                throw new CapacityExceededException();
            }

            Booking newBooking = new Booking {
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                RoomId = booking.RoomId
            };

            _context.Bookings.Add(newBooking);
            _context.SaveChanges();

            return new BookingResponse {
                bookingId = newBooking.BookingId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                Room = new RoomDto {
                    RoomId = room.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto {
                        HotelId = room.Hotel.HotelId,
                        Name = room.Hotel.Name,
                        Address = room.Hotel.Address,
                        CityId = room.Hotel.CityId,
                        CityName = room.Hotel.City.Name
                    }
                }
            };
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            var booking = _context.Bookings.Include(b => b.Room).ThenInclude(r => r.Hotel).ThenInclude(h => h.City).FirstOrDefault(b => b.BookingId == bookingId);

            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (booking.UserId != user.UserId)
            {
                throw new Exception();
            }

            return new BookingResponse {
                bookingId = booking.BookingId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                Room = new RoomDto {
                    RoomId = booking.Room.RoomId,
                    Name = booking.Room.Name,
                    Capacity = booking.Room.Capacity,
                    Image = booking.Room.Image,
                    Hotel = new HotelDto {
                        HotelId = booking.Room.Hotel.HotelId,
                        Name = booking.Room.Hotel.Name,
                        Address = booking.Room.Hotel.Address,
                        CityId = booking.Room.Hotel.CityId,
                        CityName = booking.Room.Hotel.City.Name
                    }
                }
            };
        }

        public Room GetRoomById(int RoomId)
        {
            throw new NotImplementedException();
        }

    }

}