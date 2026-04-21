namespace DocIntel.Domain.Enums;

public enum DocumentStatus
{
    Pending = 0,     // created, file not yet stored
    Queued = 1,      // file stored, waiting to process (Phase 2)
    Processing = 2,  // AI pipeline running (Phase 3)
    Processed = 3,   // fully processed, searchable
    Failed = 4       // something went wrong
}