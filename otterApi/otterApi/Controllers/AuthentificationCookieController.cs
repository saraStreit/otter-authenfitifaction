using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;
using System.Text;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

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

            var context = new UserContext();
            var users = context.Users.Where(x => x.Email == email).ToList();
            if ((users.Count > 0) && (BCryptPasswordHasher.VerifyHashedPassword(users.FirstOrDefault().Password, password)))
            {
                var claims = new Claim[]
                {
            new Claim("ID", users.FirstOrDefault().UserID.ToString()),
            new Claim("Name", users.FirstOrDefault().Name.ToString())
                };
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
