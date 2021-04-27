using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OS.API.Managers.Interfaces;
using OS.API.Models.Oversite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OS.API.Controllers.Oversite
{
    [ApiController]
    [Route(Routes.Oversite.OneOversite)]
    public class OversiteController : Controller
    {
        private readonly ILogger<OversiteController> _Logger;
        private readonly IOversiteManager _OversiteManager;

        public OversiteController(ILogger<OversiteController> logger, IOversiteManager oversiteManager)
        {
            _Logger = logger;
            _OversiteManager = oversiteManager;
        }

        [HttpGet("{oversiteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetOneOversiteAsync([FromRoute] int oversiteId)
        {
            var dbOversite = await _OversiteManager.GetModelAsync(oversiteId);

            if (dbOversite is null)
            {
                NotFound();
            }

            return Ok(dbOversite);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        public async Task<ActionResult> PostOversiteAsync([FromForm] OversiteFormData formData)
        {
            if (!Request.HasFormContentType)
            {
                return BadRequest();
            }

            var createdOversite = await _OversiteManager.CreateAsync(formData);

            return Ok(new OversiteModel { Id = createdOversite.Id });
        }
    }
}