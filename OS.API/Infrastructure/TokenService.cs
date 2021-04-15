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
using OS.API.Services;
using OS.API.Models.RefreshToken;

namespace OS.API.Infrastructure
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _Config;
        private readonly IRefreshTokenManager _RefreshTokenManager;
        private readonly ICookieService _CookieService;

        public TokenService(IConfiguration config, IRefreshTokenManager refreshTokenManager, ICookieService cookieService)
        {
            _Config = config;
            _RefreshTokenManager = refreshTokenManager;
            _CookieService = cookieService;
        }

        public async Task IssueAuthenticationTokens(HttpResponse Response, UserModel user)
        {
            var authorizationToken = GenerateAuthenticationToken(user.Username);
            _CookieService.AppendAuthenticationCookie(Response, authorizationToken);

            var refreshToken = GenerateAuthenticationRefreshToken();
            _CookieService.AppendRefreshAuthenticationCookie(Response, refreshToken);

            await _RefreshTokenManager.UpdateAsync(new RefreshTokenModel
            {
                Token = refreshToken
            });
        }


        public void RevokeAuthenticationRefreshTokens(HttpResponse Response)
        {
            _CookieService.DeleteAuthenticationCookie(Response);
            _CookieService.DeleteRefreshAuthenticationCookie(Response);
        }

        private string GenerateAuthenticationToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenClaims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _Config["Jwt:Issuer"],
                audience: _Config["Jwt:Audience"],
                claims: tokenClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateAuthenticationRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
