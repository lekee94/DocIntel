namespace DocIntel.Domain.Entities;

public class DocumentTag : Entity
{
    public Document Document { get; set; } = null!;
    public Guid DocumentId { get; set; }
    
    public Tag Tag { get; set; } = null!;
    public Guid TagId { get; set; }
}