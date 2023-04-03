using System.Security.Cryptography;
using System.Text.Json;

namespace otterApi.Controllers
{
    public class jwt_token
    {
        public string GenerateJwtKey()
        {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(key);
            }

            var base64Key = Convert.ToBase64String(key);

            var appSettings = new
            {
                Jwt = new
                {
                    Key = base64Key
                }
            };
            var json = JsonSerializer.Serialize(appSettings, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            return json;
        }
    }
}
