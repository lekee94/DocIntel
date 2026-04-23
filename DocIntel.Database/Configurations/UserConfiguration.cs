using DocIntel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocIntel.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder.OwnsOne(u => u.Email, e =>
        {
            e.Property(x => x.EmailAddress)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();
        });
        
        modelBuilder.HasMany(u => u.DocIntelDocuments).WithOne(d => d.User)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}