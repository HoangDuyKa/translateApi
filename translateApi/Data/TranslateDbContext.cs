using Microsoft.EntityFrameworkCore;
using translateApi.Models;
using static translateApi.Models.TranslateModel;

namespace translateApi.Data
{
    public class TranslateDbContext : DbContext
    {
        public TranslateDbContext(DbContextOptions<TranslateDbContext> options) : base(options)
        {
        }

        public DbSet<Translation> Translations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Translation>()
                .HasIndex(t => new { t.OriginalText, t.FromLanguage, t.ToLanguage })
                .HasDatabaseName("IX_Translation_Search");

            // Sử dụng giá trị DateTime cố định thay vì DateTime.UtcNow
            var fixedDate = new DateTime(2025, 9, 4, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Translation>().HasData(
                new Translation
                {
                    Id = 1,
                    OriginalText = "hello",
                    TranslatedText = "xin chào",
                    FromLanguage = "en",
                    ToLanguage = "vi",
                    CreatedAt = fixedDate,
                    IsActive = true
                },
                new Translation
                {
                    Id = 2,
                    OriginalText = "goodbye",
                    TranslatedText = "tạm biệt",
                    FromLanguage = "en",
                    ToLanguage = "vi",
                    CreatedAt = fixedDate,
                    IsActive = true
                },
                new Translation
                {
                    Id = 3,
                    OriginalText = "thank you",
                    TranslatedText = "cảm ơn",
                    FromLanguage = "en",
                    ToLanguage = "vi",
                    CreatedAt = fixedDate,
                    IsActive = true
                }
            );
        }
    }
}
