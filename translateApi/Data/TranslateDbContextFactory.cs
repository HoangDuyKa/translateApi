using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace translateApi.Data
{
    public class TranslateDbContextFactory : IDesignTimeDbContextFactory<TranslateDbContext>
    {
        public TranslateDbContext CreateDbContext(string[] args)
        {
            // Tạo configuration để đọc từ appsettings.json và biến môi trường
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            // Lấy connection string từ biến môi trường hoặc appsettings
            var connectionString = Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION_STRING")
                ?? configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found");

            var optionsBuilder = new DbContextOptionsBuilder<TranslateDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TranslateDbContext(optionsBuilder.Options);
        }
    }
}
