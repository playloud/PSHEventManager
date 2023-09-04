using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSHEventManager.EventManager.Model;
using PSHEventManager.EventManager.Model.PostBody;
using PSHEventManager.EventManager.Services;

namespace PSHEventManager.EventManager.Controllers
{
    [ApiController]
    [Route("/{Controller}")]
    public class EventManagerController : ControllerBase
    {
        private IConfiguration config;
        private IJwtAuth auth;

        DBServices service = DBServices.GetInstance();

        public EventManagerController(IConfiguration config, IJwtAuth auth)
        {
            this.config = config;
            this.auth = auth;
        }
        

        #region USER
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public ActionResult Authenticate([FromBody]UserCredential userCredential)
        {
            var token = auth.Authenticate(userCredential.UserName, userCredential.Password).Result;
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("create_user")]
        public async Task<ActionResult> CreateUserAccount([FromBody] UserBody userb)
        {
            var r = await DBServices.GetInstance().CreateUser(userb);
            if (r) 
                return Ok("user created");
            
            return NotFound("user creation failed");
        }

        
        [HttpGet]
        [Route("allUsers")]
        [Authorize]
        public ActionResult GetAllUsers()
        {
            var nm = User.Identity.Name;
            Console.WriteLine(nm);
            return Ok(DBServices.GetInstance().GetAllUsers());
        }

        public ActionResult UpdateUser([FromBody] UserBody user)
        {

            return Ok();
        }

        public ActionResult DeleteUser()
        {
            return Ok();
        }

        #endregion

        #region Event

        [HttpGet]
        [Authorize]
        [Route("events")]
        public ActionResult GetEvents(DateTime? date)
        {
            var results = DBServices.GetInstance().GetEventByDate(date);
            if (results.Any())
            {
                return Ok(results);
            }
            return NotFound("No Events");
        }

        //Get Event by ID
        // Endpoint: GET /events/{eventId}
        // Description: Retrieve detailed information about a specific event.
        // Parameters: eventId - The unique identifier of the event.
        // Response: An event object containing all event details.
        [HttpGet]
        [Route("events/{eventId}")]
        [Authorize]
        public ActionResult GetEvents(string eventId)
        {
            var evt = DBServices.GetInstance().GeteventById(eventId);
            if (evt != null)
                return Ok(evt);
            return NotFound("event not found");
        }

        // for debug purpose
        [HttpGet]
        [Authorize]
        [Route("allEvents")]
        public ActionResult GetAllEvents()
        {
            return Ok(DBServices.GetInstance().GetAllEvents());
        }

        [HttpPost]
        [Route("events")]
        public ActionResult CreateEvents([FromBody] EventBody eventBody)
        {
            var result = service.CreateEvent(eventBody);
            return Ok(result);
        }

        public ActionResult DeleteEvent()
        {

            return Ok();
        }

        #endregion

        #region Registration

        [HttpGet]
        [Route("registrations")]
        public ActionResult GetRegistrations(string? useremail, string? eventId)
        {
            if (useremail != null && eventId != null)
                return Ok(service.GetRegistrationByEventIdUserEmail(eventId, useremail));

            if (useremail != null)
                return Ok(service.GetRegistrationByUserEmail(useremail));

            if (eventId != null)
                return Ok(service.GetRegistrationByEventId(eventId));

            return NotFound();
        }

        [HttpPost]
        [Route("registerEvent")]
        public ActionResult CreateRegistration([FromBody] RegistrationBody body)
        {
            // find
            Event evt = service.GeteventById(body.EventId);

            if (evt == null)
                return NotFound($"Event not found id:{body.EventId}");

            User user = service.GetUserByEmail(body.Useremail);
            if (user == null)
                return NotFound($"Event not found id:{body.EventId}");

            if(service.GetRegistrationByEventIdUserEmail(body.EventId, body.Useremail).Any())
                return Conflict($"User already registered :{body.EventId}");
            
            var register = service.CreateRegistration(body.EventId, body.Useremail);
            
            return Ok(register);
        }

        [HttpPut]
        [Route("cancelRegistration")]
        public ActionResult CancelRegistration(string useremail, string eventId)
        {
            var regis = service.CancelRegistrationByEventIdUserEmail(eventId, useremail);
            if (regis.Any())
                return Ok(regis);

            return NotFound();
        }
        #endregion

        #region TEST
        [Route("test")]
        [HttpGet]
        public ActionResult Index()
        {
            var r = new
            {
                greetings = "eventManager test"
            };
            return Ok(r);
        }

        [Route("test/{id}/{secondId}")]
        [HttpGet]
        public ActionResult TestWithId(string id, int secondId)
        {
            var r = new
            {
                greetings = "eventManager test" + id + " " + secondId
            };
            return Ok(r);
        }

        #endregion









    }
}
