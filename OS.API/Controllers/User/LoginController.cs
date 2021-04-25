using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OS.API.Infrastructure.Interfaces;
using OS.API.Managers.Interfaces;
using OS.API.Models.User;
using System.Threading.Tasks;

namespace OS.API.Controllers.User
{
    [ApiController]
    [Route(Routes.User.Login)]
    [AllowAnonymous]

    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _Logger;
        private readonly IUserManager _UserManager;
        private readonly IUserDomainManager _UserDomainManager;
        private readonly ITokenService _TokenService;
        private readonly IDateService _DateService;

        public LoginController(ILogger<LoginController> logger, IUserManager userManager, IUserDomainManager userDomainManager, ITokenService tokenService, IDateService dateService)
        {
            _Logger = logger;
            _UserManager = userManager;
            _UserDomainManager = userDomainManager;
            _TokenService = tokenService;
            _DateService = dateService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthResponse>> LoginUserAsync([FromBody] AuthModel request)
        {
            var dbUser = await _UserManager.GetAuthDetails(request.Username);

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

            await _TokenService.IssueAuthenticationTokens(Response, authedUser);

            _Logger.LogInformation($"(Login) User Authenticated: {dbUser.Id}");

            var userDomains = await _UserDomainManager.GetAllByUserId(dbUser.Id);

            var response = new AuthResponse(authedUser.Id)
            {
                Username = authedUser.Username,
                LastLogin = _DateService.LastLogin(),
                Domains = userDomains
            };

            return Ok(response);
        }
    }
}
