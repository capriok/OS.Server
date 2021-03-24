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
    [Route("os/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Data.Entities.User>>> GetUsersAsync()
        {
            var userList = await _userService.GetUsersAsync();

            return Ok(userList);
        }
    }


    [Route("os/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize]
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

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserAsync(int id, PutUserScaffold scaffold)
        {
            if (id != scaffold.Id)
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
                Id = scaffold.Id,
                Username = scaffold.Username,
                Password = userEntity.Password,
                JoinDate = userEntity.JoinDate,
            };

            await _userService.UpdateUserAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserAsync(int Id, DeleteUserScaffold scaffold)
        {
            var userEntity = await _userService.GetUserModelAsync(Id);

            if (userEntity is null)
            {
                return NotFound();
            }

            if (userEntity.Id != scaffold.Id)
            {
                return BadRequest();
            }


            await _userService.DeleteUserAsync(Id);

            return NoContent();
        }
    }
}
