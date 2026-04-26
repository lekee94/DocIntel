using DocIntel.Application.DocIntelDocuments;
using DocIntel.Application.Users;
using DocIntel.Database;
using DocIntel.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompositionRoot.ServiceCollectionExtensions;

public static class DatabaseServiceCollectionExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        
        //Repositories
        services.AddScoped<IDocIntelDocumentRepository, DocIntelDocumentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}