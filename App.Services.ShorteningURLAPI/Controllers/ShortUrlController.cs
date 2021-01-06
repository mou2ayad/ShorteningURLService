using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using App.Component.ShorteningURL.Contracts;


namespace App.Services.ShorteningURLAPI.Controllers
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
        [HttpGet]
        public async Task<string> ShorteningUrlAsync(string url)
        {
            return await _shorteningService.RegisterUrlAsync(url);
        }        

    }
}
