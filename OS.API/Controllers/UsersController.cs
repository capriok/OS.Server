using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using OS.Data;
using OS.Data.Entities;


namespace OS.API.Controllers
{
    [Route("os/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly OSContext _context;

        public UsersController(OSContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            IEnumerable<User> userList = await _context.Users.ToListAsync();

            return Ok(userList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //System.Diagnostics.Debug.WriteLine(user);

            if (UserExistsByUsername(user.Username))
                return Conflict();

            DateTime JoinDate= DateTime.Now;
            string SQLFormattedJoinDate = JoinDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            user.JoinDate = SQLFormattedJoinDate;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExistsById(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExistsById(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        private bool UserExistsByUsername(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }
    }
}
