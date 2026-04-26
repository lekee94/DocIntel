using DocIntel.Application.DocIntelDocuments;
using DocIntel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocIntel.Database.Repositories;

public class DocIntelDocumentRepository(AppDbContext context) : IDocIntelDocumentRepository
{
    public async Task<DocIntelDocument?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await context.DocIntelDocuments.FirstOrDefaultAsync(d => d.Id == id, ct);

    public async Task<IReadOnlyList<DocIntelDocument>> ListByUserAsync(
        Guid userId, int page, int pageSize, CancellationToken ct = default) =>
        await context.DocIntelDocuments
            .Where(d => d.UserId == userId)
            .OrderByDescending(d => d.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

    public Task<int> CountByUserAsync(Guid userId, CancellationToken ct = default) =>
        context.DocIntelDocuments.CountAsync(d => d.UserId == userId, ct);

    public async Task AddAsync(DocIntelDocument document, CancellationToken ct = default) =>
        await context.DocIntelDocuments.AddAsync(document, ct);

    public void Remove(DocIntelDocument document) => context.DocIntelDocuments.Remove(document);

    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        context.SaveChangesAsync(ct);
}