using DocIntel.Application.Common;
using DocIntel.Application.DocIntelDocuments.Queries.GetDocIntelDocument;
using MediatR;

namespace DocIntel.Application.DocIntelDocuments.Queries.ListDocIntelDocuments;

public record ListDocIntelDocumentsQuery(Guid UserId, int Page = 1, int PageSize = 20)
    : IRequest<Result<PagedList<DocumentDto>>>;