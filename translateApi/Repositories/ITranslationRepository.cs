using static translateApi.Models.TranslateModel;

namespace translateApi.Repositories
{
    public interface ITranslationRepository
    {
        Task<IEnumerable<Translation>> GetAllAsync();
        Task<Translation?> GetByIdAsync(int id);
        Task<Translation?> FindTranslationAsync(string originalText, string fromLang, string toLang);
        Task<Translation> CreateAsync(Translation translation);
        Task<Translation?> UpdateAsync(int id, Translation translation);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Translation>> SearchAsync(string searchTerm, string? fromLang = null, string? toLang = null);
    }
}
