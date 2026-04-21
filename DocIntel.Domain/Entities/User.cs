using DocIntel.Domain.ValueObjects;

namespace DocIntel.Domain.Entities;

public class User : Entity
{
    public Email Email { get; set; } = null!;

    public string Name { get; set; } = null!;
    
    public bool IsVerified { get; set; }
    
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}