using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OS.API.Contracts;
using OS.API.Contracts.Requests.User;
using OS.API.Models.User;
using OS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult> RegisterUserAsync([FromBody] AuthRequest reqEntity)
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

            return Created("User Created", createdUser);
        }
    }
}
