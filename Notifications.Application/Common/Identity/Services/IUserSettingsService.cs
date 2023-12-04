using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Identity.Services;

public interface IUserSettingsService
{
    ValueTask<UserSettings?> GetByIdAsync(Guid userSettingsId, bool asNoTracking = false,
        CancellationToken cancellationToken = default);
}