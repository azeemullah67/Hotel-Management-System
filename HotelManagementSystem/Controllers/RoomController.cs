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
        RoomController : ControllerBase
    {
        private readonly HotelDbContext _hotelDbContext;

        public RoomController(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Room>> Get()
        {
            return await _hotelDbContext.Rooms.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await _hotelDbContext.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Room room)
        {

            var existingHotel = await _hotelDbContext.Hotels.FindAsync(room.HotelId);
            if (existingHotel == null)
            {
                return NotFound("Hotel Not Found");
            }
            var createdRoom = await _hotelDbContext.Rooms.AddAsync(room);
            await _hotelDbContext.SaveChangesAsync();

            return Ok(createdRoom.Entity);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Room room)
        {
            var existingRoom = await _hotelDbContext.Rooms.FindAsync(room.Id);
            if (existingRoom == null)
            {
                return NotFound();
            }

            existingRoom.Number = room.Number;
            existingRoom.Description = room.Description;
            existingRoom.LastBooked = room.LastBooked;
            existingRoom.Level = room.Level;

            var updatedRoom = _hotelDbContext.Update(existingRoom);
            await _hotelDbContext.SaveChangesAsync();
            return Ok(updatedRoom.Entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingRoom = await _hotelDbContext.Rooms.FindAsync(id);
            if (existingRoom == null)
            {
                return NotFound();
            }

            var removedRoom = _hotelDbContext.Rooms.Remove(existingRoom);
            await _hotelDbContext.SaveChangesAsync();

            return Ok(removedRoom.Entity);
        }
    }
}
