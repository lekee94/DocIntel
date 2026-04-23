using System.Reflection;
using DocIntel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocIntel.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<DocIntelDocument> DocIntelDocuments => Set<DocIntelDocument>();
    public DbSet<DocumentTag> DocumentTags => Set<DocumentTag>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        base.OnModelCreating(builder);
    }
}