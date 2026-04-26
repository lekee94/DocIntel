using DocIntel.Domain.Entities;
using DocIntel.Domain.Enums;
using DocIntel.Domain.ValueObjects;

namespace DocIntel.Database.Seeding;

public static class AppDbContextSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        //if (context.Users.Any())
            //return;

        var now = DateTime.UtcNow;

        // --- Tags ---
        var tags = new[]
        {
            Tag("invoice"),
            Tag("contract"),
            Tag("report"),
            Tag("receipt"),
            Tag("legal"),
            Tag("hr"),
            Tag("finance"),
            Tag("nda"),
            Tag("proposal"),
            Tag("audit"),
        };
        context.Tags.AddRange(tags);

        // --- Users ---
        var users = new[]
        {
            User("Alice Johnson",   "alice@docintel.local",   isVerified: true),
            User("Bob Martinez",    "bob@docintel.local",     isVerified: true),
            User("Carol Williams",  "carol@docintel.local",   isVerified: true),
            User("David Kim",       "david@docintel.local",   isVerified: false),
            User("Eva Novak",       "eva@docintel.local",     isVerified: true),
        };
        context.Users.AddRange(users);

        // --- Documents ---
        var documents = new[]
        {
            // Alice
            Doc(users[0], "q1-financial-report.pdf",           "application/pdf",       210_000, DocumentStatus.Processed,  now.AddDays(-30)),
            Doc(users[0], "vendor-contract-2025.pdf",          "application/pdf",       340_000, DocumentStatus.Processed,  now.AddDays(-25)),
            Doc(users[0], "employee-nda-template.docx",        "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 85_000, DocumentStatus.Processed, now.AddDays(-20)),
            Doc(users[0], "office-supplies-receipt.pdf",       "application/pdf",        12_500, DocumentStatus.Pending,    now.AddDays(-5)),

            // Bob
            Doc(users[1], "audit-summary-2024.pdf",            "application/pdf",       450_000, DocumentStatus.Processed,  now.AddDays(-60)),
            Doc(users[1], "project-proposal-alpha.pdf",        "application/pdf",       220_000, DocumentStatus.Queued,     now.AddDays(-3)),
            Doc(users[1], "travel-expense-receipt.pdf",        "application/pdf",        18_000, DocumentStatus.Processed,  now.AddDays(-15)),
            Doc(users[1], "hr-policy-handbook.pdf",            "application/pdf",       600_000, DocumentStatus.Processing, now.AddDays(-1)),

            // Carol
            Doc(users[2], "q4-budget-report.xlsx",             "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 130_000, DocumentStatus.Processed, now.AddDays(-45)),
            Doc(users[2], "software-license-agreement.pdf",    "application/pdf",       175_000, DocumentStatus.Processed,  now.AddDays(-22)),
            Doc(users[2], "marketing-proposal-q2.pptx",        "application/vnd.openxmlformats-officedocument.presentationml.presentation", 900_000, DocumentStatus.Queued, now.AddDays(-2)),
            Doc(users[2], "invoice-acme-corp-0042.pdf",        "application/pdf",        55_000, DocumentStatus.Processed,  now.AddDays(-10)),

            // David
            Doc(users[3], "contractor-agreement-draft.pdf",    "application/pdf",       240_000, DocumentStatus.Failed,     now.AddDays(-7)),
            Doc(users[3], "expense-claim-march.pdf",           "application/pdf",        22_000, DocumentStatus.Pending,    now.AddDays(-1)),

            // Eva
            Doc(users[4], "annual-compliance-report.pdf",      "application/pdf",       510_000, DocumentStatus.Processed,  now.AddDays(-90)),
            Doc(users[4], "supplier-nda-2026.pdf",             "application/pdf",        98_000, DocumentStatus.Processed,  now.AddDays(-14)),
            Doc(users[4], "payroll-audit-q1.pdf",              "application/pdf",       320_000, DocumentStatus.Processing, now.AddDays(-1)),
            Doc(users[4], "onboarding-checklist.docx",         "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 45_000, DocumentStatus.Pending, now),
        };
        context.DocIntelDocuments.AddRange(documents);

        await context.SaveChangesAsync();

        // --- Document ↔ Tag assignments ---
        var tagMap = tags.ToDictionary(t => t.Name);

        var documentTags = new List<DocumentTag>
        {
            // q1-financial-report
            Dt(documents[0],  tagMap["finance"],  now.AddDays(-30)),
            Dt(documents[0],  tagMap["report"],   now.AddDays(-30)),

            // vendor-contract-2025
            Dt(documents[1],  tagMap["contract"], now.AddDays(-25)),
            Dt(documents[1],  tagMap["legal"],    now.AddDays(-25)),

            // employee-nda-template
            Dt(documents[2],  tagMap["nda"],      now.AddDays(-20)),
            Dt(documents[2],  tagMap["hr"],       now.AddDays(-20)),
            Dt(documents[2],  tagMap["legal"],    now.AddDays(-20)),

            // office-supplies-receipt
            Dt(documents[3],  tagMap["receipt"],  now.AddDays(-5)),
            Dt(documents[3],  tagMap["finance"],  now.AddDays(-5)),

            // audit-summary-2024
            Dt(documents[4],  tagMap["audit"],    now.AddDays(-60)),
            Dt(documents[4],  tagMap["finance"],  now.AddDays(-60)),
            Dt(documents[4],  tagMap["report"],   now.AddDays(-60)),

            // project-proposal-alpha
            Dt(documents[5],  tagMap["proposal"], now.AddDays(-3)),

            // travel-expense-receipt
            Dt(documents[6],  tagMap["receipt"],  now.AddDays(-15)),
            Dt(documents[6],  tagMap["finance"],  now.AddDays(-15)),

            // hr-policy-handbook
            Dt(documents[7],  tagMap["hr"],       now.AddDays(-1)),
            Dt(documents[7],  tagMap["legal"],    now.AddDays(-1)),

            // q4-budget-report
            Dt(documents[8],  tagMap["finance"],  now.AddDays(-45)),
            Dt(documents[8],  tagMap["report"],   now.AddDays(-45)),
            Dt(documents[8],  tagMap["audit"],    now.AddDays(-45)),

            // software-license-agreement
            Dt(documents[9],  tagMap["contract"], now.AddDays(-22)),
            Dt(documents[9],  tagMap["legal"],    now.AddDays(-22)),

            // marketing-proposal-q2
            Dt(documents[10], tagMap["proposal"], now.AddDays(-2)),

            // invoice-acme-corp-0042
            Dt(documents[11], tagMap["invoice"],  now.AddDays(-10)),
            Dt(documents[11], tagMap["finance"],  now.AddDays(-10)),

            // contractor-agreement-draft
            Dt(documents[12], tagMap["contract"], now.AddDays(-7)),
            Dt(documents[12], tagMap["legal"],    now.AddDays(-7)),
            Dt(documents[12], tagMap["nda"],      now.AddDays(-7)),

            // expense-claim-march
            Dt(documents[13], tagMap["receipt"],  now.AddDays(-1)),
            Dt(documents[13], tagMap["finance"],  now.AddDays(-1)),

            // annual-compliance-report
            Dt(documents[14], tagMap["report"],   now.AddDays(-90)),
            Dt(documents[14], tagMap["legal"],    now.AddDays(-90)),
            Dt(documents[14], tagMap["audit"],    now.AddDays(-90)),

            // supplier-nda-2026
            Dt(documents[15], tagMap["nda"],      now.AddDays(-14)),
            Dt(documents[15], tagMap["contract"], now.AddDays(-14)),
            Dt(documents[15], tagMap["legal"],    now.AddDays(-14)),

            // payroll-audit-q1
            Dt(documents[16], tagMap["audit"],    now.AddDays(-1)),
            Dt(documents[16], tagMap["finance"],  now.AddDays(-1)),
            Dt(documents[16], tagMap["hr"],       now.AddDays(-1)),

            // onboarding-checklist
            Dt(documents[17], tagMap["hr"],       now),
        };

        context.DocumentTags.AddRange(documentTags);
        await context.SaveChangesAsync();
    }

    private static Tag Tag(string name) => new()
    {
        Id = Guid.NewGuid(),
        Name = name,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
    };

    private static User User(string name, string email, bool isVerified) => new()
    {
        Id = Guid.NewGuid(),
        Name = name,
        Email = new Email { EmailAddress = email },
        IsVerified = isVerified,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
    };

    private static DocIntelDocument Doc(User owner, string fileName, string contentType, long sizeBytes,
        DocumentStatus status, DateTime createdAt) => new()
    {
        Id = Guid.NewGuid(),
        FileName = new FileName { Value = fileName },
        ContentType = contentType,
        SizeBytes = sizeBytes,
        Status = status,
        UserId = owner.Id,
        CreatedAt = createdAt,
        UpdatedAt = createdAt,
    };

    private static DocumentTag Dt(DocIntelDocument doc, Tag tag, DateTime assignedAt) => new()
    {
        DocIntelDocumentId = doc.Id,
        TagId = tag.Id,
        AssignedAt = assignedAt,
    };
}
