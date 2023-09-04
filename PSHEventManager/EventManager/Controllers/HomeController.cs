using Microsoft.AspNetCore.Mvc;

namespace PSHEventManager.EventManager.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [Route("hello")]
        [HttpGet]
        public ActionResult Index()
        {
            var r = new
            {
                greetings = "helloWorld"
            };
            return Ok(r);
        }
    }
}
