using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserManager _userManager;

        public RegisterController(IUserManager userManager)
        {
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

            var newUser = new AuthModel
            {
                Username = reqEntity.Username,
                Password = reqEntity.Password
            };

            var createdUser = await _userManager.CreateAsync(newUser);

            var response = new AuthResponse
            {
                User = createdUser.Id,
            };

            return Created(createdUser.Id.ToString(), response);
        }
    }
}
