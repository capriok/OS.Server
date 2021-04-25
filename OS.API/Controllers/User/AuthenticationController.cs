using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OS.API.Infrastructure.Interfaces;
using OS.API.Managers.Interfaces;
using OS.API.Models.User;
using System.Threading.Tasks;

namespace OS.API.Controllers.User
{
    [ApiController]
    [Route(Routes.User.Authentication)]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _Config;
        private readonly ILogger _Logger;
        private readonly IUserManager _UserManager;
        private readonly IUserDomainManager _UserDomainManager;
        private readonly IRefreshTokenManager _RefreshTokenManager;
        private readonly ITokenService _TokenService;
        private readonly IDateService _DateService;

        public AuthenticationController(IConfiguration config, ILogger<AuthenticationController> logger, IUserManager userManager, IUserDomainManager userDomainManager, IRefreshTokenManager refreshTokenManager, ITokenService tokenService, IDateService dateService)
        {
            _Config = config;
            _Logger = logger;
            _UserManager = userManager;
            _UserDomainManager = userDomainManager;
            _RefreshTokenManager = refreshTokenManager;
            _TokenService = tokenService;
            _DateService = dateService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthResponse>> RefreshAuthentication()
        {
            Request.Cookies.TryGetValue(_Config["Cookie:RefreshToken"], out var refreshTokenCookie);

            _Logger.LogInformation($"Refresh Token: {refreshTokenCookie}");

            if (refreshTokenCookie is null)
            {
                return BadRequest();
            }

            var dbEntity = await _RefreshTokenManager.GetOneByTokenAsync(refreshTokenCookie);

            if (dbEntity is null)
            {
                await _TokenService.RevokeAuthenticationRefreshTokens(Response, refreshTokenCookie);
                return Unauthorized();
            }

            if (!dbEntity.Token.Equals(refreshTokenCookie))
            {
                return Unauthorized();
            }

            var authedUser = await _UserManager.GetModelAsync(dbEntity.UserId);

            await _TokenService.IssueAuthenticationTokens(Response, authedUser);

            _Logger.LogInformation($"(Token) Refreshed User Authentication: {dbEntity.UserId}");

            var userDomains = await _UserDomainManager.GetAllByUserId(authedUser.Id);

            var response = new AuthResponse(authedUser.Id)
            {
                Username = authedUser.Username,
                LastLogin = _DateService.LastLogin(),
                Domains = userDomains
            };

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RevokeAuthentication([FromBody] UserModel request)
        {
            Request.Cookies.TryGetValue(_Config["Cookie:RefreshToken"], out var refreshTokenCookie);

            var dbToken = await _RefreshTokenManager.GetOneByTokenAsync(refreshTokenCookie);

            if (dbToken is null)
            {
                return BadRequest();
            }

            if (dbToken.UserId.Equals(request.Id))
            {
                return Conflict();
            }

            await _TokenService.RevokeAuthenticationRefreshTokens(Response, refreshTokenCookie);

            _Logger.LogInformation($"(Token) Revoked User Authentication: {dbToken.UserId}");

            return Ok();
        }
    }
}
