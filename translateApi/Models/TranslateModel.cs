using System.ComponentModel.DataAnnotations;

namespace translateApi.Models
{
    public class TranslateModel
    {
        public class Translation
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [MaxLength(1000)]
            public string OriginalText { get; set; } = string.Empty;

            [Required]
            [MaxLength(1000)]
            public string TranslatedText { get; set; } = string.Empty;

            [Required]
            [MaxLength(10)]
            public string FromLanguage { get; set; } = string.Empty;

            [Required]
            [MaxLength(10)]
            public string ToLanguage { get; set; } = string.Empty;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public DateTime? UpdatedAt { get; set; }

            public bool IsActive { get; set; } = true;
        }
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
        public class TranslationCreateDto
        {
            [Required]
            [MaxLength(1000)]
            public string OriginalText { get; set; } = string.Empty;

            [Required]
            [MaxLength(1000)]
            public string TranslatedText { get; set; } = string.Empty;

            [Required]
            [MaxLength(10)]
            public string FromLanguage { get; set; } = string.Empty;

            [Required]
            [MaxLength(10)]
            public string ToLanguage { get; set; } = string.Empty;
        }
        public class TranslationUpdateDto
        {
            [MaxLength(1000)]
            public string? OriginalText { get; set; }

            [MaxLength(1000)]
            public string? TranslatedText { get; set; }

            [MaxLength(10)]
            public string? FromLanguage { get; set; }

            [MaxLength(10)]
            public string? ToLanguage { get; set; }
        }
    }
}
