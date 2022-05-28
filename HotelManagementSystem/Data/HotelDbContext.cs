using HotelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {}

        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }
    }
}
