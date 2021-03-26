using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OS.Data.Interfaces;
using OS.API.Contracts;
using OS.API.Contracts.Requests;

namespace OS.API.Controllers
{
    [ApiController]
    [Route(Routes.Auth.Register)]
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
        public async Task<ActionResult> RegisterUserAsync(UserAuthRequest reqEntity)
        {
            var dtoEntity = _userService.GetUserByUsernameAsync(reqEntity.Username);
            if (dtoEntity is not null)
            {
                return Conflict();
            }  

            var user = new Data.Entities.User
            {
                Username = reqEntity.Username,
                Password = reqEntity.Password
            };

            var createdUser = await _userService.CreateUserAsync(user);

            return Created("User Created", createdUser);
        }
    }
}
