using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using OS.API.Models.User;
using OS.API.Infrastructure.Interfaces;
using OS.API.Services.Interfaces;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Infrastructure
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _config;
        private readonly IUserManager _userManager;
        private readonly ICookieService _cookieService;

        public TokenService(IConfiguration config, IUserManager userManager, ICookieService cookieService)
        {
            _config = config;
            _userManager = userManager;
            _cookieService = cookieService;
        }

        public async Task GrantAuthenticationTokens(HttpResponse Response, UserModel user)
        {
            _cookieService.AppendUsernameCookie(Response, user.Username);

            var authorizationToken = GenerateAuthorizationToken(user.Username);
            _cookieService.AppendAuthenticationCookie(Response, authorizationToken);

            var refreshToken = GenerateAuthorizationRefreshToken();
            _cookieService.AppendRefreshAuthenticationCookie(Response, refreshToken);

            await _userManager.UpdateAsync(new UpdateModel(user.Id)
            {
                RefreshToken = refreshToken
            });
        }


        public void RevokeAuthenticationRefreshTokens(HttpResponse Response)
        {
            _cookieService.DeleteUsernameCookie(Response);
            _cookieService.DeleteAuthenticationCookie(Response);
            _cookieService.DeleteRefreshAuthenticationCookie(Response);
        }

        private string GenerateAuthorizationToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenClaims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, username),
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

        private static string GenerateAuthorizationRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
