using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Upgrade.TraineeTracking.Services.Abstractions.Services;

namespace Upgrade.TraineeTracking.Api.Controllers
{

    [ApiController]
    [Route("/")]
    public class HelloController : ControllerBase
    {

        public HelloController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello mister.");
        }
    }
}