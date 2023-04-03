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
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Umbraco.Core.Persistence.Repositories;

namespace otterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SymmetricSecurityKey _key;

        public AuthController()
        {
            var key = new byte[64];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }
            _key = new SymmetricSecurityKey(key);
        }

        [AllowAnonymous]
        [HttpPost("loginJwt")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Username != "admin" || model.Password != "1234")
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "otterApi",
                Audience = "otterClient",
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, model.Username)
                }),
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
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