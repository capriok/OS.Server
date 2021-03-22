using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OS.API.Models;
using OS.Data.Interfaces;
using OS.Data;

namespace OS.API.Controllers
{
    [Route("os/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Data.Entities.User>>> GetUsersAsync()
        {
            var userList = await _userService.GetUsersAsync();

            return Ok(userList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Data.Entities.User>> GetUserAsync(int id)
        {
            var userEntity = await _userService.GetUserModelAsync(id);
            if (userEntity is null)
            {
                return NotFound();
            }

            return Ok(userEntity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Data.Entities.User>> CreateUserAsync(NewUserScaffold newUserScaffold)
        {
            var userEntity = _userService.GetUserByUsernameAsync(newUserScaffold.Username);
            if (userEntity is not null)
            {
                return Conflict();
            }

            var userScaffold = new Data.Entities.User
            {
                Username = newUserScaffold.Username,
                Password = newUserScaffold.Password
            };

            var createdUser = await _userService.CreateUserAsync(userScaffold);

            return Created(nameof(createdUser), new { id = createdUser.Id });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserAsync(int id, PutUserScaffold putUserScaffold)
        {
            if (id != putUserScaffold.Id)
            {
                return BadRequest();
            }

            var userEntity = await _userService.GetUserEntityAsync(id);
            if (userEntity is null)
            {
                return NotFound();
            }

            var user = new Data.Entities.User
            {
                Id = id,
                Username = putUserScaffold.Username,
                Password = userEntity.Password,
                JoinDate = DateTime.Parse(putUserScaffold.JoinDate),
            };

            await _userService.UpdateUserAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Data.Entities.User>> DeleteUserAsync(int id)
        {
            var userEntity = await _userService.GetUserModelAsync(id);
            if (userEntity is null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
