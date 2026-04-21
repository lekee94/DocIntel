using DocIntel.Domain.Enums;
using DocIntel.Domain.ValueObjects;

namespace DocIntel.Domain.Entities;

public class Document : Entity
{
    public FileName FileName { get; set; } = null!;
    
    //TODO: maybe enum?
    public string ContentType { get; set; } = null!;

    public long SizeBytes { get; set; }
    
    public DocumentStatus Status { get; set; } = DocumentStatus.Pending;
    
    public User User { get; set; } = null!;
    
    public Guid UserId { get; set; }
    
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}