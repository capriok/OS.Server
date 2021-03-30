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
    [Route(Routes.User.OneUser)]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModel>> GetUserAsync([FromRoute] int id)
        {
            var reqMatch = await _userManager.GetOneModelAsync(id);
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
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] UpdateModel reqEntity)
        {
            if (id != reqEntity.Id)
            {
                return BadRequest();
            }

            var reqMatch = await _userManager.GetOneEntityAsync(id);
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

            await _userManager.UpdateAsync(updateModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int id, [FromBody] UserModel reqEntity)
        {
            if (id != reqEntity.Id)
            {
                return BadRequest();
            }

            var reqMatch = await _userManager.GetOneModelAsync(id);

            if (reqMatch is null)
            {
                return NotFound();
            }

            await _userManager.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
