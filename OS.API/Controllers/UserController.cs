using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OS.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using OS.API.Contracts;
using OS.API.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Controllers
{
    [ApiController]
    [Route(Routes.Users.AllUsers)]
    //[Authorize]
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
    }

    [ApiController]
    [Route(Routes.Users.OneUser)]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Data.Entities.User>> GetUserAsync([FromQuery] int id)
        {
            var dtoEntity = await _userService.GetUserModelAsync(id);
            if (dtoEntity is null)
            {
                return NotFound();
            }
            return Ok(dtoEntity);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserAsync([FromQuery] int id, [FromBody] UpdateEntityRequest reqEntity)
        {
            if (id != reqEntity.Id)
            {
                return BadRequest();
            }

            var dtoEntity = await _userService.GetUserEntityAsync(id);
            if (dtoEntity is null)
            {
                return NotFound();
            }

            var user = new Data.Entities.User
            {
                Id = reqEntity.Id,
                Username = reqEntity.Username,
                Password = dtoEntity.Password,
                JoinDate = dtoEntity.JoinDate,
            };

            await _userService.UpdateUserAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserAsync([FromQuery] int Id, [FromBody] DeleteEntityRequest reqEntity)
        {
            var dtoEntity = await _userService.GetUserModelAsync(Id);

            if (dtoEntity is null)
            {
                return NotFound();
            }

            if (dtoEntity.Id != reqEntity.Id)
            {
                return BadRequest();
            }


            await _userService.DeleteUserAsync(Id);

            return NoContent();
        }
    }
}
