using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Restaurant.WebApi.Security;
using System;
using System.Text;

namespace Restaurant.WebApi.DependencyInjection
{
    public static class SecurityConfiguration
    {
        public static IServiceCollection ApplicationSecurityCollectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();

            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSettings:SecretKey"]));

            var jwtAppSettingsOptions = configuration.GetSection(nameof(JwtSettings));

            // Configure JwtIssuerOptions
            services.Configure<JwtSettings>(options =>
            {
                options.Issuer = jwtAppSettingsOptions[nameof(JwtSettings.Issuer)];
                options.Audience = jwtAppSettingsOptions[nameof(JwtSettings.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingsOptions[nameof(JwtSettings.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingsOptions[nameof(JwtSettings.Audience)],

                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddScoped<IJwtFactory, JwtFactory>();

            return services;

        }
    }
}
