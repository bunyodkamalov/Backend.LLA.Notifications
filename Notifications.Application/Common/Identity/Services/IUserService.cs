using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Identity.Services;

public interface IUserService
{
    ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTacking = false,
        CancellationToken cancellationToken = default);

    ValueTask<User?> GetByIdAsync(Guid id, bool asNoTacking = false, CancellationToken cancellationToken = default);

    ValueTask<User?> GetSystemUserAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
}