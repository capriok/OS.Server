using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OS.API.Models.User;
using OS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Controllers.User
{
    [ApiController]
    [Route(Routes.User.Register)]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IUserManager _userManager;

        public RegisterController(ILogger<RegisterController> logger, IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
            }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<AuthResponse>> RegisterUserAsync([FromBody] AuthModel reqEntity)
        {
            var authEntity = _userManager.GetOneAuthDetails(reqEntity.Username);
            if (authEntity is not null)
            {
                return Conflict();
            }

            var newUser = new AuthModel(reqEntity.Id)
            {
                Username = reqEntity.Username,
                Password = reqEntity.Password
            };

            var createdUser = await _userManager.CreateAsync(newUser);

            _logger.LogInformation($"(Register) User Created: {createdUser.Id}");

            var response = new AuthResponse(createdUser.Id);

            return Created(createdUser.Id.ToString(), response);
        }
    }
}
