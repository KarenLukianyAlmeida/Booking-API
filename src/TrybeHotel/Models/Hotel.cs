namespace TrybeHotel.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// 1. Implemente as models da aplicação
public class Hotel {
    public int HotelId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    [ForeignKey("CityId")]
    public int CityId { get; set; }
    public virtual ICollection<Room>? Rooms { get; set; }
    public virtual City? City { get; set; }
}