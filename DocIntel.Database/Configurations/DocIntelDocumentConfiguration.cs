using DocIntel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocIntel.Database.Configurations;

public class DocIntelDocumentConfiguration : IEntityTypeConfiguration<DocIntelDocument>
{
    public void Configure(EntityTypeBuilder<DocIntelDocument> builder)
    {
        throw new NotImplementedException();
    }
}