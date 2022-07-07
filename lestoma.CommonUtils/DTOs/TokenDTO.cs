using System;

namespace lestoma.CommonUtils.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime ExpirationLocal => Expiration.ToLocalTime();
        public string RefreshToken { get; set; }
        public UserDTO User { get; set; }
    }
}
