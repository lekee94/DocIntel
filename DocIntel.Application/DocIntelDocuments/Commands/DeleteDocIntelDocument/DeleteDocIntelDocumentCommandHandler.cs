using DocIntel.Application.Common;
using DocIntel.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DocIntel.Application.DocIntelDocuments.Commands.DeleteDocIntelDocument;

public class DeleteDocIntelDocumentCommandHandler(
    IDocIntelDocumentRepository documentRepository,
    ILogger<DeleteDocIntelDocumentCommandHandler> logger) : IRequestHandler<DeleteDocIntelDocumentCommand, Result>
{
    public async Task<Result> Handle(DeleteDocIntelDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (document is null)
        {
            logger.LogWarning("Delete rejected: document {DocumentId} not found", request.Id);
            return Result.Failure(Error.NotFound($"Document {request.Id} not found"));
        }
        
        documentRepository.Remove(new DocIntelDocument {Id = request.Id});
        
        await documentRepository.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Document {DocumentId} deleted", request.Id);
        return Result.Success();
    }
}