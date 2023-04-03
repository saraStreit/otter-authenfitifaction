using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Security.Cryptography;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace otterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string token;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            var generator = new jwt_token();
            token = generator.GenerateJwtKey();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {

            if (model.Username != "test" || model.Password != "password")
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration[this.token]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, model.Username)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            return Ok(new { Message = $"Hello, {username}! This is a protected API endpoint." });
        }

        
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

