using System.Security.Cryptography;
using System.Text.Json;

namespace otterApi.Controllers
{
    public class JwtToken
    {
        public byte[] GenerateJwtKey()
        {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(key);
            }

            return key;
        }
    }
}
