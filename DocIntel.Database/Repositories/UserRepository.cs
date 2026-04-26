using DocIntel.Application.Users;

namespace DocIntel.Database.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken) =>
        await context.Users.FindAsync([userId, cancellationToken], cancellationToken: cancellationToken) != null;
}