using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OS.API.Contracts;
using OS.API.Contracts.Models.User;
using OS.API.Contracts.Requests.User;
using OS.API.Contracts.Responses.User;
using OS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Controllers.User
{
    [ApiController]
    [Route(Routes.User.Login)]
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
        public IActionResult LoginUserAsync([FromBody] AuthRequest reqEntity)
        {
            var authEntity = _userService.GetOneAuthDetails(reqEntity.Username);

            if (authEntity is null)
            {
                return Unauthorized();
            }

            if (!authEntity.Password.Equals(reqEntity.Password))
            {
                return Conflict();
            }

            var authedUser = new UserModel
            {
                Id = authEntity.Id,
                Username = authEntity.Username,
                JoinDate = authEntity.JoinDate
            };

            var response = new AuthReponse
            {
                Token = _tokenService.GenerateJWT(authedUser),
                Message = "Authorized",
                IsAuth = true
            };

            return Ok(response);
        }
    }
}
