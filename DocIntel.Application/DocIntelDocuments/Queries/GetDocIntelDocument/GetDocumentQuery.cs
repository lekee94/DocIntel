using DocIntel.Application.Common;
using MediatR;

namespace DocIntel.Application.DocIntelDocuments.Queries.GetDocIntelDocument;

public record GetDocumentQuery(Guid Id) : IRequest<Result<DocumentDto>>;