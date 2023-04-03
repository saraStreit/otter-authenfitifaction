using System.Security.Cryptography;
using System.Text.Json;

namespace otterApi.Controllers
{
    public class jwt_token
    {
        private const int KEY_SIZE = 256;

        public string GenerateJwtKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var key = new byte[KEY_SIZE / 8];
                rng.GetBytes(key);
                return Convert.ToBase64String(key);
            }
        }
    }
}
