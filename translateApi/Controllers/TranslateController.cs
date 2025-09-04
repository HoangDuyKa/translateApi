using Microsoft.AspNetCore.Mvc;
using translateApi.Models;
using translateApi.Repositories;
using static translateApi.Models.TranslateModel;

namespace translateApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationsController : ControllerBase
    {
        private readonly ITranslationRepository _translationRepository;

        public TranslationsController(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Translation>>> GetAllTranslations()
        {
            var translations = await _translationRepository.GetAllAsync();
            return Ok(translations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Translation>> GetTranslation(int id)
        {
            var translation = await _translationRepository.GetByIdAsync(id);
            if (translation == null)
            {
                return NotFound();
            }
            return Ok(translation);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Translation>>> SearchTranslations(
            [FromQuery] string? searchTerm,
            [FromQuery] string? fromLang,
            [FromQuery] string? toLang)
        {
            var translations = await _translationRepository.SearchAsync(searchTerm, fromLang, toLang);
            return Ok(translations);
        }

        [HttpPost]
        public async Task<ActionResult<Translation>> CreateTranslation([FromBody] TranslationCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra xem đã tồn tại chưa
            var existing = await _translationRepository
                .FindTranslationAsync(dto.OriginalText, dto.FromLanguage, dto.ToLanguage);

            if (existing != null)
            {
                return Conflict("Translation already exists");
            }

            var translation = new Translation
            {
                OriginalText = dto.OriginalText,
                TranslatedText = dto.TranslatedText,
                FromLanguage = dto.FromLanguage,
                ToLanguage = dto.ToLanguage
            };

            var created = await _translationRepository.CreateAsync(translation);
            return CreatedAtAction(nameof(GetTranslation), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Translation>> UpdateTranslation(int id, [FromBody] TranslationUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existing = await _translationRepository.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            // Cập nhật các field được cung cấp
            if (!string.IsNullOrWhiteSpace(dto.OriginalText))
                existing.OriginalText = dto.OriginalText;

            if (!string.IsNullOrWhiteSpace(dto.TranslatedText))
                existing.TranslatedText = dto.TranslatedText;

            if (!string.IsNullOrWhiteSpace(dto.FromLanguage))
                existing.FromLanguage = dto.FromLanguage;

            if (!string.IsNullOrWhiteSpace(dto.ToLanguage))
                existing.ToLanguage = dto.ToLanguage;

            var updated = await _translationRepository.UpdateAsync(id, existing);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTranslation(int id)
        {
            var success = await _translationRepository.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
