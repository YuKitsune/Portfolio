using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Portfolio.Application
{
    public class TokenService
    {
        private readonly string issuer;
        private readonly string audience;
        private readonly string secret;

        public TokenService(string issuer, string audience, string secret)
        {
            this.issuer = issuer;
            this.audience = audience;
            this.secret = secret;
        }
        
        public string GenerateToken(string userId)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}