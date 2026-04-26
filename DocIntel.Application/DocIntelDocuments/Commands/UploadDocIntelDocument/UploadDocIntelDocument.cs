using DocIntel.Application.Common;
using MediatR;

namespace DocIntel.Application.DocIntelDocuments.Commands.UploadDocIntelDocument;

public record UploadDocumentCommand(
    string FileName,
    string ContentType,
    long SizeBytes,
    Guid UserId) : IRequest<Result<Guid>>;