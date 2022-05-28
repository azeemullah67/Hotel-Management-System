using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class 
        HotelController : ControllerBase
    {
        private readonly HotelDbContext _hotelDbContext;

        public HotelController(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Hotel>> Get()
        {
            return await _hotelDbContext.Hotels.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var hotel = await _hotelDbContext.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Hotel hotel)
        {
            var createdHotel = await _hotelDbContext.Hotels.AddAsync(hotel);
            await _hotelDbContext.SaveChangesAsync();

            return Ok(createdHotel.Entity);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Hotel hotel)
        {
            var existingHotel = await _hotelDbContext.Hotels.FindAsync(hotel.Id);
            if (existingHotel == null)
            {
                return NotFound();
            }

            existingHotel.Name = hotel.Name;
            existingHotel.Description = hotel.Description;
            existingHotel.NumberOfRooms = hotel.NumberOfRooms;
            existingHotel.Email = hotel.Email;
            existingHotel.TelNo = hotel.TelNo;
            existingHotel.Address = hotel.Address;

            var updatedRoom = _hotelDbContext.Update(existingHotel);
            await _hotelDbContext.SaveChangesAsync();
            return Ok(updatedRoom.Entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingHotel = await _hotelDbContext.Hotels.FindAsync(id);
            if (existingHotel == null)
            {
                return NotFound();
            }

            var removedRoom = _hotelDbContext.Hotels.Remove(existingHotel);
            await _hotelDbContext.SaveChangesAsync();

            return Ok(removedRoom.Entity);
        }
    }
}
