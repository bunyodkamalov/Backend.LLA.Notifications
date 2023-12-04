using Notifications.Application.Common.Models.Querying;
using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Notifications.Services;

public interface IEmailHistoryService
{
    Task<List<EmailHistory>> GetByFilterAsync(FilterPagination filterPagination, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<EmailHistory> CreateAsync(EmailHistory emailHistory, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}