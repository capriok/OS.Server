using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OS.API.Managers.Interfaces;
using OS.API.Models.RefreshToken;
using OS.API.Models.User;
using System;
using System.Threading.Tasks;

namespace OS.API.Controllers.User
{
    [ApiController]
    [Route(Routes.User.Register)]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _Logger;
        private readonly IUserManager _UserManager;
        private readonly IRefreshTokenManager _RefreshTokenManager;

        public RegisterController(ILogger<RegisterController> logger, IUserManager userManager, IRefreshTokenManager refreshTokenManager)
        {
            _Logger = logger;
            _UserManager = userManager;
            _RefreshTokenManager = refreshTokenManager;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<AuthResponse>> RegisterUserAsync([FromBody] AuthModel request)
        {
            var dbUser = await _UserManager.GetAuthDetails(request.Username);
            if (dbUser is not null)
            {
                return Conflict();
            }

            var createdUser = await _UserManager.CreateAsync(new AuthModel(request.Id)
            {
                Username = request.Username,
                Password = request.Password
            });

             await _RefreshTokenManager.CreateAsync(new RefreshTokenModel
            {
                Token = Guid.NewGuid().ToString(),
                UserId = createdUser.Id
            });

            var response = new AuthResponse(createdUser.Id);

            return Created(createdUser.Id.ToString(), response);
        }
    }
}
