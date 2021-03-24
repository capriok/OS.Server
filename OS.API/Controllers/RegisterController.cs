using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OS.API.Models;
using OS.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace OS.API.Controllers
{
    [Route("os/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Core.Models.User>> CreateUserAsync(NewUserScaffold scaffold)
        {
            var userEntity = _userService.GetUserByUsernameAsync(scaffold.Username);
            if (userEntity is not null)
            {
                return Conflict();
            }

            var userScaffold = new Data.Entities.User
            {
                Username = scaffold.Username,
                Password = scaffold.Password
            };

            var createdUser = await _userService.CreateUserAsync(userScaffold);

            return Created(nameof(createdUser), createdUser);
        }
    }
}
