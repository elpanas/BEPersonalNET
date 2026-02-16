using Microsoft.AspNetCore.Mvc;

namespace BEPersonal.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Hello from Docker!");
    }
}
