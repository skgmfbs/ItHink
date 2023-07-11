using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [ApiController]
    [Route("api/commands/[controller]")]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController(){}

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("Test inbound!!!");
            
            return Ok("Test inbound!!!");
        }
    }
}