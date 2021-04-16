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
    [Route(Routes.Oversite.AllOversites)]
    public class OversitesController : ControllerBase
    {
       private readonly IOversiteManager _OversiteManager;

        public OversitesController(IOversiteManager oversiteManager)
        {
            _OversiteManager = oversiteManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OversiteModel>> AllOversitesAsync()
        {
            var dbOversite = await _OversiteManager.GetAllAsync();

            return Ok(dbOversite);
        }
    }
}
