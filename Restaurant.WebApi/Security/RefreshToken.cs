using System;

namespace Restaurant.WebApi.Security
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
        public DateTime Created { get; set; }
        public string GeneratedBy { get; set; }
    }
}
