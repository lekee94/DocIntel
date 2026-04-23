using DocIntel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocIntel.Database.Configurations;

public class DocumentTagConfiguration : IEntityTypeConfiguration<DocumentTag>
{
    public void Configure(EntityTypeBuilder<DocumentTag> modelBuilder)
    {
        modelBuilder.HasKey(dt => new { dt.DocIntelDocumentId, dt.TagId });

        modelBuilder.HasOne(dt => dt.DocIntelDocument)
                .WithMany(d => d.DocumentTags)
                .HasForeignKey(dt => dt.DocIntelDocumentId)
                .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.HasOne(dt => dt.Tag)
                .WithMany(t => t.DocumentTags)
                .HasForeignKey(dt => dt.TagId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}