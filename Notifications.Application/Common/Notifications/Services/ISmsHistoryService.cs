using Notifications.Application.Common.Models.Querying;
using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Notifications.Services;

public interface ISmsHistoryService
{
    ValueTask<IList<SmsHistory>> GetByFilterAsync(FilterPagination filterPagination, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<SmsHistory> CreateAsync(SmsHistory smsHistory, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}