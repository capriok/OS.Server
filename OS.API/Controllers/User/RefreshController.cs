using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OS.API.Contracts;
using OS.API.Contracts.Models.User;
using OS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Controllers.User
{
    [ApiController]
    [Route(Routes.User.Refresh)]
    [AllowAnonymous]
    public class RefreshController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public RefreshController(IConfiguration config, IUserService userService, ITokenService tokenService)
        {
            _config = config;
            _userService = userService;
            _tokenService = tokenService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult RefreshAuthToken()
        {

            Request.Cookies.TryGetValue(_config["Cookie:Username"], out var usernameCookie);
            Request.Cookies.TryGetValue(_config["Cookie:RefreshToken"], out var refreshTokenCookie);

            if (usernameCookie is not null && refreshTokenCookie is not null)
            {
                return Unauthorized();
            }

            var authEntity = _userService.GetOneAuthDetails(usernameCookie);

            if (!(authEntity is null && authEntity.RefreshToken.Equals(refreshTokenCookie)))
            {
                return Unauthorized();
            }

            var authedUser = new UserModel
            {
                Id = authEntity.Id,
                Username = authEntity.Username
            };

            _tokenService.GrantAuthorizationTokens(Response, authedUser);

            return Ok();
        }
    }
}
