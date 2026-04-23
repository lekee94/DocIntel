using DocIntel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocIntel.Database.Configurations;

public class DocIntelDocumentConfiguration : IEntityTypeConfiguration<DocIntelDocument>
{
    public void Configure(EntityTypeBuilder<DocIntelDocument> modelBuilder)
    {
        modelBuilder.OwnsOne(d => d.FileName, fn =>
            {
                fn.Property(f => f.Value)
                    .HasColumnName("FileName")
                    .HasMaxLength(255)
                    .IsRequired();
            });

        modelBuilder.Property(d => d.ContentType).HasMaxLength(100).IsRequired();
        modelBuilder.Property(d => d.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

        modelBuilder.HasIndex(d => d.UserId);
        modelBuilder.HasIndex(d => d.CreatedAt);
    }
}