using Microsoft.IdentityModel.Tokens;
using JaVisitei.Brasil.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;

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

            if (user.UserRole.Name is null)
                throw new ArgumentNullException(nameof(user.UserRole.Name));

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, Environment.GetEnvironmentVariable("JWT_SUBJECT")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("username", user.Username),
                new Claim("role", user.UserRole.Name)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIDED_MINUTE"))),
                signingCredentials: credenciais);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateAuthenticationRefreshToken(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (user.Email is null)
                throw new ArgumentNullException(nameof(user.Email));

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, Environment.GetEnvironmentVariable("JWT_SUBJECT")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("email", user.Email)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIDED_TIME"))),
                signingCredentials: credenciais);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
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
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var account = jwtToken.Claims.First(x => x.Type == "email").Value;

                return account;
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
