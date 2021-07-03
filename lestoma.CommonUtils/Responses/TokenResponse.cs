using System;

namespace lestoma.CommonUtils.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime ExpirationLocal => Expiration.ToLocalTime();
        public UserResponse User { get; set; }
    }
}
