using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;

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

        [HttpGet(Name = "GetLogin")]
        public string Get()
        {
            return "hello world";
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Your authentication logic here
            bool isAuthenticated = false;
            if (username == "user" && password == "password")
            {
                isAuthenticated = true;
            }

            if (!isAuthenticated)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return Ok();
        }
    }
}
