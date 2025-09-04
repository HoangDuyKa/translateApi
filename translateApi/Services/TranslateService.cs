using translateApi.Interfaces;
using translateApi.Models;
using static translateApi.Models.TranslateModel;

namespace translateApi.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly Dictionary<string, Dictionary<string, string>> _translations;

        public TranslateService()
        {
            // Tạo một số bản dịch mẫu (trong thực tế bạn sẽ tích hợp với Google Translate API hoặc Azure Translator)
            _translations = new Dictionary<string, Dictionary<string, string>>
            {
                ["en"] = new Dictionary<string, string>
                {
                    ["hello"] = "xin chào",
                    ["goodbye"] = "tạm biệt",
                    ["thank you"] = "cảm ơn",
                    ["yes"] = "có",
                    ["no"] = "không"
                },
                ["vi"] = new Dictionary<string, string>
                {
                    ["xin chào"] = "hello",
                    ["tạm biệt"] = "goodbye",
                    ["cảm ơn"] = "thank you",
                    ["có"] = "yes",
                    ["không"] = "no"
                }
            };
        }

        public Task<TranslateResponse> TranslateAsync(TranslateRequest request)
        {
            try
            {
                var response = new TranslateResponse();

                // Đơn giản hóa: phát hiện ngôn ngữ và dịch
                var detectedLang = DetectLanguage(request.Text);
                response.DetectedLanguage = detectedLang;

                // Thực hiện dịch
                var translatedText = Translate(request.Text.ToLower(), detectedLang, request.ToLanguage);

                if (!string.IsNullOrEmpty(translatedText))
                {
                    response.TranslatedText = translatedText;
                    response.Success = true;
                }
                else
                {
                    response.TranslatedText = request.Text;
                    response.Success = false;
                    response.ErrorMessage = "Translation not found";
                }

                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new TranslateResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        private string DetectLanguage(string text)
        {
            // Logic đơn giản để phát hiện ngôn ngữ
            if (text.All(c => c <= 127)) // ASCII characters
                return "en";
            return "vi";
        }

        private string Translate(string text, string fromLang, string toLang)
        {
            if (fromLang == toLang)
                return text;

            if (_translations.ContainsKey(fromLang) &&
                _translations[fromLang].ContainsKey(text))
            {
                return _translations[fromLang][text];
            }

            return string.Empty;
        }
    }
}
