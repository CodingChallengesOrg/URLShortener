using Microsoft.AspNetCore.Mvc;
using URLShortener.Services;

namespace URLShortener.Controllers
{
    [ApiController]
    [Route("r")]
    public class RedirectController : ControllerBase
    {
        private readonly UrlService _urlService;

        public RedirectController(UrlService urlService)
        {
            _urlService = urlService;
        }

        // GET: /r/{code}
        [HttpGet("{code}")]
        public async Task<IActionResult> RedirectToOriginal(string code)
        {
            var entity = await _urlService.ResolveAsync(code);
            if (entity == null)
                return NotFound("Short URL not found");

            return Redirect(entity.OriginalUrl);
        }
    }
}
