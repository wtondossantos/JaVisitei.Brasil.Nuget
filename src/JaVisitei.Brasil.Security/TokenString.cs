using Microsoft.IdentityModel.Tokens;
using JaVisitei.Brasil.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace JaVisitei.Brasil.Security
{
    public static class TokenString
    {
        public static string GenerateAuthenticationToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, Environment.GetEnvironmentVariable("JWT_SUBJECT")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.Username),
                new Claim("email", user.Email),
                new Claim("role", user.UserRole.Name)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIDED_MINUTE"))),
                signingCredentials: credenciais);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
