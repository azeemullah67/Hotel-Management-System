using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Bogus;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly HotelDbContext _hotelDbContext;
        public ProfileController(HotelDbContext hotelDbContext, IConfiguration configuration)
        {
            this._hotelDbContext = hotelDbContext;
        }

        [HttpGet]
#pragma warning disable 1998
        public async Task<IEnumerable<Profile>> Get()
#pragma warning restore 1998
        {
            return await _hotelDbContext.Profiles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var profile = await _hotelDbContext.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Profile profile)
        {
            var createdProfile = await _hotelDbContext.Profiles.AddAsync(profile);
            await _hotelDbContext.SaveChangesAsync();

            return Ok(createdProfile.Entity);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Profile profile)
        {
            var existingProfile = await _hotelDbContext.Profiles.FindAsync(profile.Id);
            if (existingProfile == null)
            {
                return NotFound();
            }

            existingProfile.Ref = profile.Ref;
            existingProfile.Forename = profile.Forename;
            existingProfile.Surname = profile.Surname;
            existingProfile.Email = profile.Email;
            existingProfile.DateOfBirth = profile.DateOfBirth;
            existingProfile.TelNo = profile.TelNo;

            var updatedProfile = _hotelDbContext.Update(existingProfile);
            await _hotelDbContext.SaveChangesAsync();
            return Ok(updatedProfile.Entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProfile = await _hotelDbContext.Profiles.FindAsync(id);
            if (existingProfile == null)
            {
                return NotFound();
            }

            var removedProfile = _hotelDbContext.Profiles.Remove(existingProfile);
            await _hotelDbContext.SaveChangesAsync();

            return Ok(removedProfile.Entity);
        }

        [HttpPost("GenerateAndInsert")]
        public async Task<IActionResult> GenerateAndInsert([FromBody] int count = 1000)
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            var profiles = GenerateProfiles(count);
            var gererationTime = s.Elapsed.ToString();
            s.Restart();

            _hotelDbContext.Profiles.AddRange(profiles);
            var insertedCount = await _hotelDbContext.SaveChangesAsync();

            return Ok(new {
                inserted = insertedCount,
                generationTime = gererationTime,
                insertTime = s.Elapsed.ToString()
            });
        }

        private IEnumerable<Profile> GenerateProfiles(int count)
        {
            var salutations = new string[] {"Mr", "Mrs"};

            var profileGenerator = new Faker<Profile>()
                .RuleFor(p => p.Ref, v => v.Person.UserName)
                .RuleFor(p => p.Forename, v => v.Person.FirstName)
                .RuleFor(p => p.Surname, v => v.Person.LastName)
                .RuleFor(p => p.Email, v => v.Person.Email)
                .RuleFor(p => p.TelNo, v => v.Person.Phone)
                .RuleFor(p => p.DateOfBirth, v => v.Person.DateOfBirth)
                .RuleFor(p => p.Salutation, v => v.PickRandom(salutations))
                .RuleFor(p => p.Address, v => v.Address.FullAddress())
                .RuleFor(p => p.Country, v => v.Address.Country());

            return profileGenerator.Generate(count);
        }
    }
}
