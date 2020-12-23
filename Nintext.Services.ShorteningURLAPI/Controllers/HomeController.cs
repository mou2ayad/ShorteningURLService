
using Microsoft.AspNetCore.Mvc;

using Nintex.NetCore.Component.ShorteningURL.Contracts;

namespace Nintex.Services.ShorteningURLAPI.Controllers
{
    [Route("/ntx/")]
    public class HomeController : Controller
    {                
        public HomeController()
        {        
            
        }
        [HttpGet("{key}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Redirection([FromServices] IShorteningService _shorteningService, string key)
        {
            var url = _shorteningService.GetActualUrlByShortkey(key);
            if (string.IsNullOrEmpty(url))                            
                return NotFound();                        
            return RedirectPermanent(url);

        }    
    }
}
