using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OS.API.Managers.Interfaces;
using OS.API.Models.User;
using System.Threading.Tasks;

namespace OS.API.Controllers.User
{
    [ApiController]
    [Route(Routes.User.OneUser)]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _UserManager;

        public UserController(IUserManager userManager)
        {
            _UserManager = userManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModel>> GetUserAsync([FromRoute] int userId)
        {
            var dbUser = await _UserManager.GetModelAsync(userId);

            if (dbUser is null)
            {
                return NotFound();
            }

            return Ok(dbUser);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int userId, [FromBody] UpdateModel user)
        {
            var dbUser = await _UserManager.GetModelAsync(userId);

            if (userId != dbUser.Id)
            {
                return BadRequest();
            }

            var dbUbserAuth = await _UserManager.GetAuthDetails(dbUser.Username);

            if (dbUbserAuth is null)
            {
                return NotFound();
            }

            var updateModel = new UpdateModel(dbUbserAuth.Id)
            {
                Username = user.Username,
                Password = dbUbserAuth.Password
            };

            await _UserManager.UpdateAsync(updateModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int userId, [FromBody] UserModel user)
        {
            if (userId != user.Id)
            {
                return BadRequest();
            }

            var dbUser = await _UserManager.GetModelAsync(userId);

            if (dbUser is null)
            {
                return NotFound();
            }

            await _UserManager.DeleteAsync(userId);

            return NoContent();
        }
    }
}
