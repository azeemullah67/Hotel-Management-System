using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        public ProfileController()
        {
        }

        [HttpGet]
#pragma warning disable 1998
        public async Task<string> Get()
#pragma warning restore 1998
        {
            return "Test";
        }
    }
}
