using DocIntel.Application.Common;
using MediatR;

namespace DocIntel.Application.DocIntelDocuments.Queries.GetDocIntelDocument;

public class GetDocumentHandler(IDocIntelDocumentRepository repository)
    : IRequestHandler<GetDocumentQuery, Result<DocumentDto>>
{
    public async Task<Result<DocumentDto>> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
    {
        var document = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (document is null)
            return Result<DocumentDto>.Failure(Error.NotFound($"Document {request.Id} not found"));

        var dto = new DocumentDto(
            document.Id,
            document.FileName.Value,
            document.ContentType,
            document.SizeBytes,
            document.Status.ToString(),
            document.UserId,
            document.CreatedAt,
            document.Tags.Select(t => t.Name).ToList());

        return Result<DocumentDto>.Success(dto);
    }
}