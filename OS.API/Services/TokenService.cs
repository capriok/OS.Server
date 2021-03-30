using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using OS.API.Services.Interfaces;
using OS.API.Contracts.Models.User;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly ICookieService _cookieService;

        public TokenService(IConfiguration config, IUserService userService, ICookieService cookieService)
        {
            _config = config;
            _userService = userService;
            _cookieService = cookieService;
        }

        private string GenerateAuthorizationRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        private string GenerateAuthorizationToken(int id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenClaims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: tokenClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void GrantAuthorizationTokens(HttpResponse Response, UserModel user)
        {
            GrantUsernameToken(Response, user.Username);
            GrantJWTAuthToken(Response, user.Id);
            GrantAuthorizationRefreshToken(Response, user.Id);
        }

        private void GrantUsernameToken(HttpResponse Response, string username)
        {
            _cookieService.AppendUsernameCookie(Response, username);
        }
        private void GrantJWTAuthToken(HttpResponse Response, int id)
        {
            var authorizationToken = GenerateAuthorizationToken(id);

            _cookieService.AppendAuthorizationCookie(Response, authorizationToken);
        }

        private void GrantAuthorizationRefreshToken(HttpResponse Response, int id)
        {
            var refreshToken = GenerateAuthorizationRefreshToken();

            var user = new UpdateModel
            {
                Id = id,
                RefreshToken = refreshToken,
            };

            _userService.UpdateAsync(user);
            _cookieService.AppendAuthorizationRefreshCookie(Response, refreshToken);
        }
    }
}
