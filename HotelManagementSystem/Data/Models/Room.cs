using System;

namespace HotelManagementSystem.Data.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public int HotelId { get; set; }

        public string Description { get; set; }

        public DateTime LastBooked { get; set; }

        public int Level { get; set; }
    }
}
