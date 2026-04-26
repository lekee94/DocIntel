using DocIntel.Application.DocIntelDocuments.Commands.UploadDocIntelDocument;
using DocIntel.Application.DocIntelDocuments.Queries.GetDocIntelDocument;
using MediatR;

namespace DocIntel.Api.Endpoints;

public static class DocumentsEndpoints
{
    public static void MapDocumentsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/documents")
            .WithTags("Documents");
        
        group.MapPost("/", UploadDocument)
            .WithName("UploadDocument")
            .WithSummary("Upload a new document")
            .WithDescription("Accepts a document file and queues it for processing.")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet("/{id:guid}", GetDocument)
            .WithName("GetDocument")
            .WithSummary("Get a single document by ID")
            .Produces<DocumentDto>()
            .Produces(StatusCodes.Status404NotFound);
    }
    
    private static async Task<IResult> UploadDocument(
        UploadDocumentCommand command,
        ISender sender,
        CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return result.IsSuccess
            ? Results.Created($"/documents/{result.Value}", result.Value)
            : result.Error.Code switch
            {
                "NotFound" => Results.NotFound(result.Error),
                "Validation" => Results.BadRequest(result.Error),
                _ => Results.Problem(result.Error.Message)
            };
    }

    private static async Task<IResult> GetDocument(
        Guid id,
        ISender sender,
        CancellationToken ct)
    {
        var result = await sender.Send(new GetDocumentQuery(id), ct);
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.NotFound(result.Error);
    }
}