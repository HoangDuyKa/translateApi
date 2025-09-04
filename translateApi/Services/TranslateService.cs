using translateApi.Interfaces;
using translateApi.Models;
using translateApi.Repositories;
using static translateApi.Models.TranslateModel;

namespace translateApi.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly ITranslationRepository _translationRepository;

        public TranslateService(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        public async Task<TranslateResponse> TranslateAsync(TranslateRequest request)
        {
            try
            {
                var response = new TranslateResponse();

                // Phát hiện ngôn ngữ nếu là auto
                var fromLang = request.FromLanguage == "auto"
                    ? DetectLanguage(request.Text)
                    : request.FromLanguage;

                response.DetectedLanguage = fromLang;

                // Tìm bản dịch trong database
                var translation = await _translationRepository
                    .FindTranslationAsync(request.Text, fromLang, request.ToLanguage);

                if (translation != null)
                {
                    response.TranslatedText = translation.TranslatedText;
                    response.Success = true;
                }
                else
                {
                    // Nếu không tìm thấy, trả về text gốc
                    response.TranslatedText = request.Text;
                    response.Success = false;
                    response.ErrorMessage = "Translation not found in database";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new TranslateResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        private string DetectLanguage(string text)
        {
            // Logic đơn giản để phát hiện ngôn ngữ
            if (text.All(c => c <= 127)) // ASCII characters
                return "en";
            return "vi";
        }
    }
}
