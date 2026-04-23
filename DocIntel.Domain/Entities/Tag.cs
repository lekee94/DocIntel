namespace DocIntel.Domain.Entities;

public class Tag : Entity
{
    //TODO: maybe enum?
    public string Name { get; set; } = null!;
    
    public ICollection<DocIntelDocument> DocIntelDocuments { get; set; } = new List<DocIntelDocument>();
    
    public ICollection<DocumentTag> DocumentTags { get; set; } = new List<DocumentTag>();
}