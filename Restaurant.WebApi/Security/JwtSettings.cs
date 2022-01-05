using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Restaurant.WebApi.Security
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }

        public DateTime Expiration => IssuedAt.Add(ValidFor);

        public DateTime NotBefore => DateTime.Now;

        public DateTime IssuedAt => DateTime.Now;

        public TimeSpan ValidFor { get; set; } = TimeSpan.FromHours(72);

        public Func<Task<string>> JtiGenerator =>
            () => Task.FromResult(Guid.NewGuid().ToString());

        public SigningCredentials SigningCredentials { get; set; }
    }
}
