using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nintex.NetCore.Component.ShorteningURL.Contracts;


namespace Nintex.Services.ShorteningURLAPI.Controllers
{

    [Route("api/ShorteningUrl")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShorteningService _shorteningService;
        public ShortUrlController(IShorteningService shorteningService)
        {
            _shorteningService = shorteningService;
        }
        [HttpGet("{url}")]
        public async Task<string> ShorteningUrlAsync(string url)
        {
            return await _shorteningService.RegisterUrlAsync(url);
        }        

    }
}
