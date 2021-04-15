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
       private readonly IOversiteManager _OversiteManager;

        public OversiteController(IOversiteManager oversiteManager)
        {
            _OversiteManager = oversiteManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OversiteModel>> GetOversiteAsync([FromRoute] int oversiteId)
        {
            var dbOversite = await _OversiteManager.GetEntityAsync(oversiteId);
            if (dbOversite is null)
            {
                return NotFound();
            }
            return Ok(dbOversite);
        }
    }
}
