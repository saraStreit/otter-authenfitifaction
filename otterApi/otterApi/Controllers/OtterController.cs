using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace otterApi.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class SessionController : ControllerBase
        {
            [HttpPost("set")]
            public IActionResult SetSessionValue(string key, string value)
            {
                HttpContext.Session.SetString(key, value);
                return Ok();
            }
        }
}
