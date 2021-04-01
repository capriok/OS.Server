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
        private readonly ILogger<LoginController> _logger;
        private readonly IUserManager _userManager;
        private readonly ITokenService _tokenService;
        private readonly IDateService _dateService;

        public LoginController(ILogger<LoginController> logger, IUserManager userManager, ITokenService tokenService, IDateService dateService)
        {
            _logger = logger;
            _userManager = userManager;
            _tokenService = tokenService;
            _dateService = dateService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<AuthResponse> LoginUserAsync([FromBody] AuthModel reqEntity)
        {
            var authEntity = _userManager.GetOneAuthDetails(reqEntity.Username);

            if (authEntity is null)
            {
                return Unauthorized();
            }

            if (!authEntity.Password.Equals(reqEntity.Password))
            {
                return Conflict();
            }

            var authedUser = new UserModel(authEntity.Id)
            {
                Username = authEntity.Username
            };

            _tokenService.GrantAuthenticationTokens(Response, authedUser);

            _logger.LogInformation($"(Login) User Authenticated: {authEntity.Id}");

            var response = new AuthResponse(authedUser.Id)
            {
                LastLogin = _dateService.LastLogin()
            };

            return Ok(response);
        }
    }
}
