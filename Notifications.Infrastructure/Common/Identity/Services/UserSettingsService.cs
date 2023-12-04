using Notifications.Application.Common.Identity.Services;
using Notifications.Domain.Entities;
using Notifications.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Common.Identity.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUserSettingsRepository _userSettingsRepository;

    public UserSettingsService(IUserSettingsRepository userSettingsRepository)
    {
        _userSettingsRepository = userSettingsRepository;
    }

    public  ValueTask<UserSettings?> GetByIdAsync(Guid userSettingsId, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
         _userSettingsRepository.GetByIdAsync(userSettingsId, asNoTracking, cancellationToken);
}