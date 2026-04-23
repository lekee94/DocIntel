using DocIntel.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CompositionRoot;

public static class DatabaseServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite("Server=NTB-368;Database=DocIntel;Trusted_Connection=True;TrustServerCertificate=True;");
        });

        return services;
    }
}