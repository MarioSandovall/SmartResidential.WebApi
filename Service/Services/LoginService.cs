using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.Models.Login;
using Model.Settings;
using Service.Interfaces;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public LoginService(
            IOptions<JwtConfiguration> jwtOptions)
        {
            _jwtConfiguration = jwtOptions.Value;
        }

        public string BuildToken(UserLogin user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key);

            var claims = new List<Claim>
            {
                new Claim(ClaimsTypeCustom.Id, user.Id.ToString(), ClaimValueTypes.Integer32)
            };

            AddRoles(user, ref claims);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private void AddRoles(UserLogin user, ref List<Claim> claims)
        {
            if (user.Roles != null)
            {
                foreach (var roleName in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName, ClaimValueTypes.String));
                }
            }

            if (user.RoleIds != null)
            {
                foreach (var roleId in user.RoleIds)
                {
                    claims.Add(new Claim(ClaimsTypeCustom.RoleId, roleId.ToString(), ClaimValueTypes.Integer32));
                }
            }
        }
    }

}
