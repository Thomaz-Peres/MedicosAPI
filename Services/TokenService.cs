using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Text;
using DesafioMedicos.Models;
using Microsoft.IdentityModel.Tokens;

namespace DesafioMedicos.Services
{
    public static class TokenService
    {
        //  gera um token que receber um user.
        public static string GenerateToken(User user)
        {
            // resposavel por gerar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                //  ele usa a chave novamente, e gera baseado em um algortimo chamado Sha256
                SigningCredentials =  new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Cria o token, conforme ele foi passado no "tokenDescriptor"
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // Gera a string do token
            return tokenHandler.WriteToken(token);
        }
    }
}