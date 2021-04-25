using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OS.API.Models.Oversite;
using OS.API.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Controllers.Oversite
{
    [ApiController]
    [Route(Routes.Oversite.SearchResult)]
    [AllowAnonymous]

    public class SearchResultController : ControllerBase
    {
        private readonly ILogger<SearchResultController> _Logger;
        private readonly IOversiteManager _OversiteManager;

        public SearchResultController(ILogger<SearchResultController> logger, IOversiteManager oversiteManager)
        {
            _Logger = logger;
            _OversiteManager = oversiteManager;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OversiteModel>> SearchResultOversitesAsync([FromBody] SearchResultRequest request)
        {
            _Logger.LogInformation(request.SearchResult);

            var osList = await _OversiteManager.GetBySearchResultAsync(request.SearchResult);

            return Ok(osList);
        }
    }
}
