using DocIntel.Application.Common;
using DocIntel.Application.Users;
using DocIntel.Domain.Entities;
using DocIntel.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DocIntel.Application.DocIntelDocuments.Commands.UploadDocIntelDocument;

public class UploadDocIntelDocumentHandler(
    IDocIntelDocumentRepository documentRepository,
    IUserRepository userRepository,
    ILogger<UploadDocIntelDocumentHandler> logger) : IRequestHandler<UploadDocumentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        if (!await userRepository.ExistsAsync(request.UserId, cancellationToken))
            return Result<Guid>.Failure(Error.NotFound($"User {request.UserId} not found"));

        var document = new DocIntelDocument
        {
            FileName = new FileName
            {
                Value = request.FileName
            },
            ContentType = request.ContentType,
            SizeBytes = request.SizeBytes,
            UserId = request.UserId
        };

        await documentRepository.AddAsync(document, cancellationToken);
        await documentRepository.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Document {DocumentId} uploaded by {RequestUserId}", document.Id, request.UserId);

        return Result<Guid>.Success(document.Id);
    }
}