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

namespace OS.API.Controllers.User
{
    [ApiController]
    [Route(Routes.User.OneUser)]
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
        public async Task<ActionResult<UserModel>> GetUserAsync([FromRoute] int id)
        {
            var reqMatch = await _userService.GetOneModelAsync(id);
            if (reqMatch is null)
            {
                return NotFound();
            }
            return Ok(reqMatch);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] UpdateRequest reqEntity)
        {
            if (id != reqEntity.Id)
            {
                return BadRequest();
            }

            var reqMatch = await _userService.GetOneEntityAsync(id);
            if (reqMatch is null)
            {
                return NotFound();
            }

            var updateModel = new UpdateModel
            {
                Id = reqMatch.Id,
                Username = reqEntity.Username,
                Password = reqMatch.Password
            };

            await _userService.UpdateAsync(updateModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int id, [FromBody] DeleteRequest reqEntity)
        {
            if (id != reqEntity.Id)
            {
                return BadRequest();
            }

            var reqMatch = await _userService.GetOneModelAsync(id);

            if (reqMatch is null)
            {
                return NotFound();
            }

            await _userService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
