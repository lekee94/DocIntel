using DocIntel.Domain.Entities;

namespace DocIntel.Application.DocIntelDocuments;

public interface IDocIntelDocumentRepository
{
    Task<DocIntelDocument?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<DocIntelDocument>> ListByUserAsync(
        Guid userId, int page, int pageSize, CancellationToken ct = default);
    Task<int> CountByUserAsync(Guid userId, CancellationToken ct = default);
    Task AddAsync(DocIntelDocument document, CancellationToken ct = default);
    void Remove(DocIntelDocument document);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}