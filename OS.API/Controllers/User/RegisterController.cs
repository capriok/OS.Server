using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OS.API.Contracts;
using OS.API.Contracts.Requests.User;
using OS.API.Contracts.Models.User;
using OS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OS.API.Contracts.Responses.User;

namespace OS.API.Controllers.User
{
    [ApiController]
    [Route(Routes.User.Register)]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<AuthResponse>> RegisterUserAsync([FromBody] AuthRequest reqEntity)
        {
            var authEntity = _userService.GetOneAuthDetails(reqEntity.Username);
            if (authEntity is not null)
            {
                return Conflict();
            }

            var newUser = new AuthModel
            {
                Username = reqEntity.Username,
                Password = reqEntity.Password
            };

            var createdUser = await _userService.CreateAsync(newUser);

            var response = new AuthResponse
            {
                User = createdUser.Id,
            };

            return Created(createdUser.Id.ToString(), response);
        }
    }
}
