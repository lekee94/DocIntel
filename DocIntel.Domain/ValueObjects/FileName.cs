namespace DocIntel.Domain.ValueObjects;

public class FileName : ValueObject
{
    public string Value { get; set; } = null!;
    
    public string Extension => Path.GetExtension(Value).ToLowerInvariant();
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}