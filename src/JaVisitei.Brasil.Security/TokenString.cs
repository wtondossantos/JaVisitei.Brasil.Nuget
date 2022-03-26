using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JaVisitei.Brasil.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace JaVisitei.Brasil.Security
{
    public class TokenString
    {
        public IConfiguration _configuration;
        private readonly Usuario _usuario;

        public TokenString(Usuario usuario, IConfiguration configuration)
        {
            _usuario = usuario;
            _configuration = configuration;
        }

        public string GerarToken()
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, Environment.GetEnvironmentVariable("JWT_SUBJECT")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Id", _usuario.Id.ToString()),
                new Claim("Nome", _usuario.Nome),
                new Claim("NomeUsuario", _usuario.NomeUsuario),
                new Claim("Email", _usuario.Email)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credenciais);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
