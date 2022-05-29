using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Data.Models
{
    public class NewReservation
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        public int ProfileId { get; set; }

        [Required]
        public DateTime? From { get; set; }

        [Required]
        public DateTime? To { get; set; }
    }
}
