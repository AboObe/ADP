using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_TOKEN = "wRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        public const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<UserAccount> users;

        public JwtTokenHandler()
        {
            users = new List<UserAccount> {
            new UserAccount { UserName = "Ubai", Password = "ubai123" },
            new UserAccount { UserName = "Ayas", Password = "ayas123" }
            };
        }

        public AuthenticationResponse? GenerateJWTToken(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
                return null;
            var userAccount = users.Where(x => x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password ).FirstOrDefault();
            if (userAccount == null)
                return null;


            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_TOKEN);
            var claimIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,authenticationRequest.UserName)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
                );

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                UserName = userAccount.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };
        }


    }
}
