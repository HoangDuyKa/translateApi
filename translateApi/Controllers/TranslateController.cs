using Microsoft.AspNetCore.Mvc;
using translateApi.Interfaces;
using translateApi.Models;
using translateApi.Services;
using static translateApi.Models.TranslateModel;

namespace translateApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslateController : ControllerBase
    {
        private readonly ITranslateService _translateService;

        public TranslateController(ITranslateService translateService)
        {
            _translateService = translateService;
        }

        [HttpPost]
        public async Task<ActionResult<TranslateResponse>> Translate([FromBody] TranslateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Text))
            {
                return BadRequest(new TranslateResponse
                {
                    Success = false,
                    ErrorMessage = "Text is required"
                });
            }

            var result = await _translateService.TranslateAsync(request);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("languages")]
        public ActionResult<object> GetSupportedLanguages()
        {
            return Ok(new
            {
                languages = new[]
                {
                    new { code = "en", name = "English" },
                    new { code = "vi", name = "Tiếng Việt" },
                    new { code = "auto", name = "Auto Detect" }
                }
            });
        }
    }
}
