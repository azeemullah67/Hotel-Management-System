using System;
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
    public class ReservationsController : ControllerBase
    {
        private readonly HotelDbContext _hotelDbContext;

        public ReservationsController(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Reservation>> Get()
        {
            return await _hotelDbContext.Reservations.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reservation = await _hotelDbContext.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewReservation newReservation)
        {
            var room = await _hotelDbContext.Rooms.FirstOrDefaultAsync(r => r.Id == newReservation.RoomId);
            var profile = await _hotelDbContext.Profiles.FirstOrDefaultAsync(p => p.Id == newReservation.ProfileId);

            if (room == null || profile == null)
            {
                return BadRequest();
            }

            if (newReservation.From != null && newReservation.To != null && 
                DateTime.UtcNow < newReservation.From && DateTime.UtcNow < newReservation.To && 
                newReservation.To > newReservation.From)
            {
                var reservation = new Reservation
                {
                    Created = DateTime.UtcNow,
                    From = newReservation.From.Value,
                    To = newReservation.To.Value,
                    RoomId = room.Id,
                    Profile = profile
                };

                var createdReservation = await _hotelDbContext.Reservations.AddAsync(reservation);
                await _hotelDbContext.SaveChangesAsync();

                return Ok(createdReservation.Entity.Id);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingRoomReservation = await _hotelDbContext.Reservations.FindAsync(id);
            if (existingRoomReservation == null)
            {
                return NotFound();
            }

            var removedRoom = _hotelDbContext.Reservations.Remove(existingRoomReservation);
            await _hotelDbContext.SaveChangesAsync();

            return Ok(removedRoom.Entity);
        }
    }
}
