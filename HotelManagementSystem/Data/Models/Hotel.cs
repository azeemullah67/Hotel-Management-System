namespace HotelManagementSystem.Data.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string TelNo { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int NumberOfRooms { get; set; }

        public int PricePerNight { get; set; }

        public string Facilities { get; set; }
    }
}
