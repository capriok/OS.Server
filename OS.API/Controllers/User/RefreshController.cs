using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OS.API.Models.User;
using OS.API.Infrastructure.Interfaces;
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
        private readonly IUserManager _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger _log;

        public RefreshController(IConfiguration config, IUserManager userManager, ITokenService tokenService, ILogger<RefreshController> log)
        {
            _config = config;
            _userManager = userManager;
            _tokenService = tokenService;
            _log = log;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<AuthResponse> RefreshAuthToken()
        {
            Request.Cookies.TryGetValue(_config["Cookie:Username"], out var usernameCookie);
            Request.Cookies.TryGetValue(_config["Cookie:RefreshToken"], out var refreshTokenCookie);

            _log.LogInformation($"Refresh attempt by: {usernameCookie}");

            if (usernameCookie is null && refreshTokenCookie is null)
            {
                return Unauthorized();
            }

            var authEntity = _userManager.GetOneAuthDetails(usernameCookie);

            if (authEntity is null && !authEntity.RefreshToken.Equals(refreshTokenCookie))
            {
                return Conflict();
            }

            _log.LogInformation("Attempt Valid, Granting Tokens");

            var authedUser = new UserModel
            {
                Id = authEntity.Id,
                Username = authEntity.Username
            };

            _tokenService.GrantAuthorizationTokens(Response, authedUser);

            return Ok( new AuthResponse
            {
                User = authedUser.Id,
                LastLogin = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm"),
            });
        }
    }
}
