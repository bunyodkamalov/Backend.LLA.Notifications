using System.Linq.Expressions;
using Notifications.Domain.Entities;

namespace Notifications.Persistence.Repositories.Interfaces;

public interface IUserRepository
{
    IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = default, 
        bool asNoTracking = false 
        );

    ValueTask<IList<User>> GetByIdsAsync(
        IEnumerable<Guid> usersId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );
}