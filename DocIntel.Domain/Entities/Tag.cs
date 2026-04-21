namespace DocIntel.Domain.Entities;

public class Tag : Entity
{
    //TODO: maybe enum?
    public string Name { get; set; } = null!;
    
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}