using Notifications.Domain.Entities;
using Notifications.Persistence.DataContext;
using Notifications.Persistence.Repositories.Interfaces;

namespace Notifications.Persistence.Repositories;

public class UserSettingsRepository : EntityRepositoryBase<UserSettings, NotificationDbContext>, IUserSettingsRepository
{
    public UserSettingsRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public ValueTask<UserSettings?> GetByIdAsync(Guid userId, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => base.GetByIdAsync(userId, asNoTracking, cancellationToken);


}