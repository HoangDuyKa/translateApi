using static translateApi.Models.TranslateModel;

namespace translateApi.Interfaces
{
    public interface ITranslateService
    {
        Task<TranslateResponse> TranslateAsync(TranslateRequest request);
    }
}
