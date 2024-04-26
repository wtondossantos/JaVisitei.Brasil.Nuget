using Microsoft.IdentityModel.Tokens;
using JaVisitei.Brasil.Data.Entities;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Collections.Generic;
using System.Collections;

namespace JaVisitei.Brasil.Security
{
    public static class TokenString
    {
        public static string GenerateAuthenticationToken(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (user.Username is null)
                throw new ArgumentNullException(nameof(user.Username));

            if (user.UserRole is null)
                throw new ArgumentNullException(nameof(user.UserRole));

            if (user.Email is null)
                throw new ArgumentNullException(nameof(user.Email));

            if (user.UserRole.Name is null)
                throw new ArgumentNullException(nameof(user.UserRole.Name));
            
            var claims = new Dictionary<string, object> {
                [JwtRegisteredClaimNames.Sub] = Environment.GetEnvironmentVariable("JWT_SUBJECT"),
                [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
                [JwtRegisteredClaimNames.Iat] = DateTime.UtcNow.ToString(),
                ["id"] = user.Id.ToString(),
                ["username"] = user.Username,
                ["role"] = user.UserRole.Name,
                [ClaimTypes.Email] = user.Email
            };

            return GenerateToken(claims);
        }

        public static string GenerateAuthenticationRefreshToken(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (user.Email is null)
                throw new ArgumentNullException(nameof(user.Email));

            var claims = new Dictionary<string, object>
            {
                [JwtRegisteredClaimNames.Sub] = Environment.GetEnvironmentVariable("JWT_SUBJECT"),
                [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
                [JwtRegisteredClaimNames.Iat] = DateTime.UtcNow.ToString(),
                ["id"] = user.Id.ToString(),
                [ClaimTypes.Email] = user.Email
            };

            return GenerateToken(claims);
        }

        private static string GenerateToken(Dictionary<string, object> claims)
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                Claims = claims,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIDED_MINUTE"))),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"))), SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JsonWebTokenHandler
            {
                SetDefaultTimesOnTokenCreation = false
            };

            return handler.CreateToken(descriptor);
        }

        public static string ValidateJwtToken(string token)
        {
            var tokenHandler = new JsonWebTokenHandler();
            
            try
            {
                var jwtToken = tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"))),
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                    ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                    RequireExpirationTime = true
                });

                if(jwtToken.Result.IsValid)
                    return jwtToken.Result.Claims["id"].ToString();

                return null;
            }
            catch
            {
                return null;
            }
        }

        public static string GenerateEmailConfirmationToken()
        {
            return JaVisitei.Brasil.Helper.Others.Utility.RandomHexString("X8");
        }

        public static string GeneratePasswordResetToken()
        {
            return JaVisitei.Brasil.Helper.Others.Utility.RandomAlphanumericString(8);
        }
    }
}
