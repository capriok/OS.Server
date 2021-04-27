using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OS.API.Models.Oversite;
using OS.API.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Controllers.Oversite
{
    [ApiController]
    [Route(Routes.Oversite.RecentlyFounded)]
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
        public async Task<ActionResult<OversiteModel>> RecentlyFoundedOversitesAsync()
        {
            // replace with logic to respond with oversites that meet a recent data req
            // paginate results to 10 ish

            var osList = await _OversiteManager.GetRecentAsync();

            return Ok(osList);
        }
    }
}