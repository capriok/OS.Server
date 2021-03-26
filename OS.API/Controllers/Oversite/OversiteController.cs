using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OS.API.Contracts;
using OS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Controllers.Oversite
{

    [ApiController]
    [Route(Routes.Oversite.AllOversites)]
    [Authorize]
    public class OversitesController : ControllerBase
    {
       private readonly IOversiteService _oversiteService;

        public OversitesController(IOversiteService oversiteService)
        {
            _oversiteService = oversiteService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OversiteModel>> AllUsersAsync()
        {
            var reqMatch = await _oversiteService.GetAllAsync();
            if (reqMatch is null)
            {
                return NotFound();
            }
            return Ok(reqMatch);
        }
    }
}
