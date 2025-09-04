using Microsoft.EntityFrameworkCore;
using translateApi.Data;
using translateApi.Models;
using static translateApi.Models.TranslateModel;

namespace translateApi.Repositories
{
    public class TranslationRepository : ITranslationRepository
    {
        private readonly TranslateDbContext _context;

        public TranslationRepository(TranslateDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Translation>> GetAllAsync()
        {
            return await _context.Translations
                .Where(t => t.IsActive)
                .OrderBy(t => t.OriginalText)
                .ToListAsync();
        }

        public async Task<Translation?> GetByIdAsync(int id)
        {
            return await _context.Translations
                .FirstOrDefaultAsync(t => t.Id == id && t.IsActive);
        }

        public async Task<Translation?> FindTranslationAsync(string originalText, string fromLang, string toLang)
        {
            return await _context.Translations
                .FirstOrDefaultAsync(t =>
                    t.OriginalText.ToLower() == originalText.ToLower() &&
                    t.FromLanguage == fromLang &&
                    t.ToLanguage == toLang &&
                    t.IsActive);
        }

        public async Task<Translation> CreateAsync(Translation translation)
        {
            _context.Translations.Add(translation);
            await _context.SaveChangesAsync();
            return translation;
        }

        public async Task<Translation?> UpdateAsync(int id, Translation translation)
        {
            var existingTranslation = await GetByIdAsync(id);
            if (existingTranslation == null) return null;

            existingTranslation.OriginalText = translation.OriginalText;
            existingTranslation.TranslatedText = translation.TranslatedText;
            existingTranslation.FromLanguage = translation.FromLanguage;
            existingTranslation.ToLanguage = translation.ToLanguage;
            existingTranslation.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingTranslation;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var translation = await GetByIdAsync(id);
            if (translation == null) return false;

            translation.IsActive = false;
            translation.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Translation>> SearchAsync(string searchTerm, string? fromLang = null, string? toLang = null)
        {
            var query = _context.Translations.Where(t => t.IsActive);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(t =>
                    t.OriginalText.Contains(searchTerm) ||
                    t.TranslatedText.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(fromLang))
            {
                query = query.Where(t => t.FromLanguage == fromLang);
            }

            if (!string.IsNullOrWhiteSpace(toLang))
            {
                query = query.Where(t => t.ToLanguage == toLang);
            }

            return await query.OrderBy(t => t.OriginalText).ToListAsync();
        }
    }
}
