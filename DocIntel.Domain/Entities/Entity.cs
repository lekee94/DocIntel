namespace DocIntel.Domain.Entities;

public class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    
    public override bool Equals(object? obj) => obj is Entity other && Equals(other);

    public override int GetHashCode() => Id.GetHashCode();
    
    private bool Equals(Entity other) => Id == other.Id;
}