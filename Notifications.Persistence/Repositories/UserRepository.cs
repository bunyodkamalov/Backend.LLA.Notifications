using System.Linq.Expressions;
using Notifications.Domain.Entities;
using Notifications.Persistence.DataContext;
using Notifications.Persistence.Repositories.Interfaces;

namespace Notifications.Persistence.Repositories;

public class UserRepository : EntityRepositoryBase<User, NotificationDbContext>, IUserRepository
{
    public UserRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = default,
        bool asNoTracking = false
    ) =>
        base.Get(predicate, asNoTracking);


    public async ValueTask<IList<User>> GetByIdsAsync(
        IEnumerable<Guid> usersId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) =>
        await base.GetByIdsAsync(usersId, asNoTracking, cancellationToken);


    public ValueTask<User?> GetByIdAsync(
        Guid userId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) => 
        base.GetByIdAsync(userId, asNoTracking, cancellationToken);
}