using Newtonsoft.Json;
using System;

namespace lestoma.CommonUtils.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime ExpirationLocal => Expiration.ToLocalTime();
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public UserResponse User { get; set; }
    }
}
