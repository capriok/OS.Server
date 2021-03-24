using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OS.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using OS.Data.Interfaces;
using System.Security.Claims;

namespace OS.API.Controllers
{
    [Route("os/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public LoginController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Login([FromBody] LoginUserScaffold scaffold)
        {
            var userEntity = _userService.GetUserByUsernameAsync(scaffold.Username);

            if (userEntity is null)
            {
                return Unauthorized();
            }

            if (!userEntity.Password.Equals(scaffold.Password))
            {
                return Conflict();
            }

            var user = new Core.Models.User
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                JoinDate = userEntity.JoinDate
            };

            var token = new { token = GenerateJSONWebToken(user) };

            return Ok(token);
        }

        private string GenerateJSONWebToken(Core.Models.User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenClaims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("joinDate", user.JoinDate.ToString("")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: tokenClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
