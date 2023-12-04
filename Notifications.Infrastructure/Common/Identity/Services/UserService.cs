using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Identity.Services;
using Notifications.Domain.Entities;
using Notifications.Domain.Enums;
using Notifications.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Common.Identity.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTacking = false,
        CancellationToken cancellationToken = default)
        => _userRepository.GetByIdsAsync(ids, asNoTacking, cancellationToken);

    public ValueTask<User?> GetByIdAsync(Guid id, bool asNoTacking = false,
        CancellationToken cancellationToken = default)
        => _userRepository.GetByIdAsync(id, asNoTacking, cancellationToken);

    public async ValueTask<User?> GetSystemUserAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await _userRepository.Get(user => user.Role == RoleType.System, asNoTracking)
            .Include(user => user.UserSettings)
            .SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }
}