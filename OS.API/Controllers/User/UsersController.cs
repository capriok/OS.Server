﻿using Microsoft.AspNetCore.Authorization;
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
    [Route(Routes.User.AllUsers)]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersAsync()
        {
            var allUsers = await _userManager.GetAllAsync();

            return Ok(allUsers);
        }
    }
}
