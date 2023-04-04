using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace otterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthentificationController : ControllerBase
    {
        private readonly ILogger<AuthentificationController> _logger;

        public AuthentificationController(ILogger<AuthentificationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("login")]
        [Produces("application/json")]
        public IActionResult TryLogin([FromHeader(Name = "Authorisation")] string header)
        {

            string emailPwSecret = header.Substring("Basic ".Length).Trim();
            string emailPw = Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(emailPwSecret));
            int separator = emailPw.IndexOf(":");
            string email = emailPw.Substring(0, separator);
            string pw = emailPw.Substring(separator + 1);

            if (email == "admin" && pw == "1234")
            {
                return Ok(new { message = "Login successful" });
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
