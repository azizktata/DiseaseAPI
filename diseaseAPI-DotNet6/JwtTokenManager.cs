using Domain.Interfaces;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace diseaseAPI_DotNet6
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _UnitOfWork;
        public JwtTokenManager(IConfiguration configuration, IUnitOfWork unitOfWork) 
        {
            _configuration= configuration;
            _UnitOfWork = unitOfWork;

        }
        public string Authenticate(string username, string password)
        {
            var admin = _UnitOfWork.Admin.Find(a => a.username== username && a.password == password).ToList();
            if (admin.Count==0)
                return string.Empty;

            var key = _configuration.GetValue<string>("JwtConfig:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            
        }
    }
}
