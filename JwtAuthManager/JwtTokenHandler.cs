using JwtAuthManager.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthManager
{
    public class JwtTokenHandler
    {
        public const string Jwt_Secret_Key = "05B1FC55-E34A-4A13-91E3-3DC030221317";
        private const int Jwt_Token_Validity_Mins = 20;
        private readonly List<UserAccount> _users;
        public JwtTokenHandler()
        {
            _users = new List<UserAccount>
            {
                new UserAccount { UserName = "admin", Password = "admin", Role = "Administrator" },
                new UserAccount { UserName = "user1", Password = "user1", Role = "User" },
                new UserAccount { UserName = "user2", Password = "user2", Role = "User" }
            };
        }
        public AuthResponse? GenerateJwtToken(AuthRequest request)
        {
            if(string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return null;
            }
            var user = _users.SingleOrDefault(x => x.UserName == request.UserName && x.Password == request.Password);
            if(user == null)
            {
                return null;
            }
            var tokenExpiration = DateTime.UtcNow.AddMinutes(Jwt_Token_Validity_Mins);
            var tokenKey = Encoding.ASCII.GetBytes(Jwt_Secret_Key);
            var claims = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            });
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = tokenExpiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthResponse
            {
                UserName = user.UserName,
                Token = tokenHandler.WriteToken(token),
                ExpiresIn = Jwt_Token_Validity_Mins * 60
            };

        }
    }
}
