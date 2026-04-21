using System.ComponentModel.DataAnnotations;

namespace DocIntel.Domain.ValueObjects;

public class Email : ValueObject
{
    [EmailAddress]
    public string EmailAddress { get; set; } = null!;

    public override string ToString() => EmailAddress;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return EmailAddress;
    }
}