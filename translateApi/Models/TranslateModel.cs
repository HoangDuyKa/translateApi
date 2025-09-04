namespace translateApi.Models
{
    public class TranslateModel
    {
        public class TranslateRequest
        {
            public string Text { get; set; } = string.Empty;
            public string FromLanguage { get; set; } = "auto";
            public string ToLanguage { get; set; } = "vi";
        }

        public class TranslateResponse
        {
            public string TranslatedText { get; set; } = string.Empty;
            public string DetectedLanguage { get; set; } = string.Empty;
            public bool Success { get; set; }
            public string ErrorMessage { get; set; } = string.Empty;
        }
    }
}
