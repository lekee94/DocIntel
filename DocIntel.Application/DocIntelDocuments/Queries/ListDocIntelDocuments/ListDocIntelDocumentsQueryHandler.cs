using DocIntel.Application.Common;
using DocIntel.Application.DocIntelDocuments.Queries.GetDocIntelDocument;
using MediatR;

namespace DocIntel.Application.DocIntelDocuments.Queries.ListDocIntelDocuments;

public class ListDocIntelDocumentsQueryHandler(IDocIntelDocumentRepository documentRepository)
    : IRequestHandler<ListDocIntelDocumentsQuery, Result<PagedList<DocumentDto>>>
{
    public async Task<Result<PagedList<DocumentDto>>> Handle(ListDocIntelDocumentsQuery request,
        CancellationToken cancellationToken)
    {
        var totalCount = await documentRepository.CountByUserAsync(request.UserId, cancellationToken);

        var pagedDocuments =
            await documentRepository.ListByUserAsync(request.UserId, request.Page, request.PageSize, cancellationToken);
        
        if(pagedDocuments.Count == 0)
            return Result<PagedList<DocumentDto>>.Failure(Error.NotFound($"Documents not found for user {request.UserId}"));

        var dtos = pagedDocuments.Select(d => new DocumentDto(
            d.Id,
            d.FileName.Value,
            d.ContentType,
            d.SizeBytes,
            d.Status.ToString(),
            d.UserId,
            d.CreatedAt,
            d.Tags.Select(t => t.Name).ToList()
        )).ToList();
        
        var page = new PagedList<DocumentDto>(dtos, request.Page, request.PageSize, totalCount);

        return Result<PagedList<DocumentDto>>.Success(page);
    }
}