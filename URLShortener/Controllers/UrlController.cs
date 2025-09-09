namespace URLShortener.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using URLShortener.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly UrlService _urlService;
        private readonly IConfiguration _config;

        public UrlController(UrlService urlService, IConfiguration config)
        {
            _urlService = urlService;
            _config = config;
        }

        // POST: /api/url/shorten
        [HttpPost("shorten")]
        public async Task<IActionResult> Shorten([FromBody] UrlRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.LongUrl))
                return BadRequest("Invalid URL");

            var result = await _urlService.CreateShortUrlAsync(request.LongUrl);

            // Build full short URL
            var baseUrl = _config["AppSettings:BaseUrl"] ?? $"{Request.Scheme}://{Request.Host}";
            var shortUrl = $"{baseUrl}/r/{result.ShortUrl}";

            return Ok(new { shortUrl, originalUrl = result.OriginalUrl });
        }
    }

    // Request DTO
    public class UrlRequest
    {
        public string LongUrl { get; set; } = string.Empty;
    }
}
