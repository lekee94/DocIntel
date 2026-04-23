namespace DocIntel.Domain.Entities;

public class DocumentTag
{
    public DocIntelDocument DocIntelDocument { get; set; } = null!;
    public Guid DocIntelDocumentId { get; set; }
    
    public Tag Tag { get; set; } = null!;
    public Guid TagId { get; set; }

    public DateTime AssignedAt { get; set; }
}