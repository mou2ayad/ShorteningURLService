using Microsoft.AspNetCore.Mvc;
using App.Services.ShorteningURL.KeysGenerationService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.ShorteningURL.KeysGenerationService.Controllers
{
    [ApiController]

    public class KGSContoller : ControllerBase
    {
        public KGSContoller()
        {

        }
        [HttpGet("api/kgs/keys")]
        public List<string> GetKeys([FromServices] IKeysQueue keysQueue)
        {
            return keysQueue.Dequeue();
        }

    }
}
