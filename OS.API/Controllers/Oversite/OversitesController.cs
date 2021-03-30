using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OS.API.Models.Oversite;
using OS.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Controllers.Oversite
{

    [ApiController]
    [Route(Routes.Oversite.OneOversite)]
    [Authorize]
    public class OversiteController : ControllerBase
    {
       private readonly IOversiteService _oversiteService;

        public OversiteController(IOversiteService oversiteService)
        {
            _oversiteService = oversiteService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OversiteModel>> GetUserAsync([FromRoute] int id)
        {
            var reqMatch = await _oversiteService.GetOneEntityAsync(id);
            if (reqMatch is null)
            {
                return NotFound();
            }
            return Ok(reqMatch);
        }
    }
}
