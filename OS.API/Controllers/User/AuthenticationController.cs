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
using OS.API.Services;
using OS.API.Models.RefreshToken;

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
        private readonly IRefreshTokenManager _RefreshTokenManager;
        private readonly ITokenService _TokenService;
        private readonly IDateService _DateService;

        public AuthenticationController(IConfiguration config, ILogger<AuthenticationController> logger, IUserManager userManager, IRefreshTokenManager refreshTokenManager, ITokenService tokenService, IDateService dateService)
        {
            _Config = config;
            _Logger = logger;
            _UserManager = userManager;
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

            //await _TokenService.IssueAuthenticationTokens(Response, new UserModel(1) { Username = "kyle"});
            //return Ok(new AuthResponse(1) {LastLogin = _DateService.LastLogin()});

            if (refreshTokenCookie is null)
            {
                return BadRequest();
            }

            var dbEntity = await _RefreshTokenManager.GetOneAsync(refreshTokenCookie);
            
            _Logger.LogInformation(dbEntity.Token.ToString());
            _Logger.LogInformation(dbEntity.UserId.ToString());

            if (dbEntity is null)
            {
                _TokenService.RevokeAuthenticationRefreshTokens(Response);
                return Unauthorized();
            }

            if (!dbEntity.Token.Equals(refreshTokenCookie))
            {
                return Unauthorized();
            }
             
            var authedUser = await _UserManager.GetModelAsync(dbEntity.UserId);

            await _TokenService.IssueAuthenticationTokens(Response, authedUser);

            _Logger.LogInformation($"(Token) Refreshed User Authentication: {dbEntity.UserId}");

            return Ok(new AuthResponse(authedUser.Id)
            {
                LastLogin = _DateService.LastLogin()
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RevokeAuthentication([FromBody] UserModel request)
        {
            Request.Cookies.TryGetValue(_Config["Cookie:RefreshToken"], out var refreshTokenCookie);

            //_TokenService.RevokeAuthenticationRefreshTokens(Response);
            //return Ok();

            var dbEntity = await _RefreshTokenManager.GetOneAsync(refreshTokenCookie);

            if (dbEntity is null)
            {
                return BadRequest();
            }

            if (!dbEntity.UserId.Equals(request.Id))
            {
                return Conflict();
            }

            _TokenService.RevokeAuthenticationRefreshTokens(Response);

            _Logger.LogInformation($"(Token) Revoked User Authentication: {dbEntity.UserId}");

            return Ok();
        }
    }
}
