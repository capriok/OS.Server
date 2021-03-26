using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OS.API.Contracts;
using OS.API.Contracts.Requests;
using OS.API.Contracts.Responses;
using OS.API.Services.Interfaces;
using OS.Data.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OS.API.Controllers
{
    [ApiController]
    [Route(Routes.Auth.Login)]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public LoginController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult LoginUserAsync([FromBody] UserAuthRequest reqEntity)
        {
            var dtoEntity = _userService.GetUserByUsernameAsync(reqEntity.Username);

            if (dtoEntity is null)
            {
                return Unauthorized();
            }

            if (!dtoEntity.Password.Equals(reqEntity.Password))
            {
                return Conflict();
            }

            var user = new Core.Models.User
            {
                Id = dtoEntity.Id,
                Username = dtoEntity.Username,
                JoinDate = dtoEntity.JoinDate
            };

            var response = new UserAuthReponse
            {
                Token = _tokenService.GenerateJWT(user),
                Message = "Authorized",
                IsAuth = true
            };

            return Ok(response);
        }
    }
}
