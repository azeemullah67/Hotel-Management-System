using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public int RoomId { get; set; }

        public Profile Profiles { get; set; }

        public DateTime Created { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
