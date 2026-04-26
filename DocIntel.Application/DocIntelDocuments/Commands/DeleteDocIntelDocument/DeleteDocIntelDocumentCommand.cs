using DocIntel.Application.Common;
using MediatR;

namespace DocIntel.Application.DocIntelDocuments.Commands.DeleteDocIntelDocument;

public record DeleteDocIntelDocumentCommand(Guid Id) : IRequest<Result>;
