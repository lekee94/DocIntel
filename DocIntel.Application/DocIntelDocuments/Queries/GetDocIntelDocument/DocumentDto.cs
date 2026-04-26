namespace DocIntel.Application.DocIntelDocuments.Queries.GetDocIntelDocument;

public record DocumentDto(
    Guid Id,
    string FileName,
    string ContentType,
    long SizeBytes,
    string Status,
    Guid UserId,
    DateTime UploadedAt,
    IReadOnlyList<string> Tags);