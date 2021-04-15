using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OS.API.Models.User;
using OS.API.Infrastructure.Interfaces;
using OS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OS.API.Controllers.User
{
    [ApiController]
    [Route(Routes.User.Login)]
    [AllowAnonymous]

    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _Logger;
        private readonly IUserManager _UserManager;
        private readonly IRefreshTokenManager _RefreshTokenManager;
        private readonly ITokenService _TokenService;
        private readonly IDateService _DateService;

        public LoginController(ILogger<LoginController> logger, IUserManager userManager, IRefreshTokenManager refreshTokenManager,  ITokenService tokenService, IDateService dateService)
        {
            _Logger = logger;
            _UserManager = userManager;
            _RefreshTokenManager = refreshTokenManager;
            _TokenService = tokenService;
            _DateService = dateService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthResponse>> LoginUserAsync([FromBody] AuthModel request)
        {
            var dbUser = _UserManager.GetAuthDetails(request.Username);

            if (dbUser is null)
            {
                return Unauthorized();
            }

            if (!dbUser.Password.Equals(request.Password))
            {
                return Conflict();
            }

            var authedUser = new UserModel(dbUser.Id)
            {
                Username = dbUser.Username
            };

            _TokenService.IssueAuthenticationTokens(Response, authedUser);

            _Logger.LogInformation($"(Login) User Authenticated: {dbUser.Id}");

            var response = new AuthResponse(authedUser.Id)
            {
                LastLogin = _DateService.LastLogin()
            };

            return Ok(response);
        }
    }
}
