using Notifications.Application.Common.Models.Querying;
using Notifications.Domain.Entities;
using Notifications.Domain.Enums;

namespace Notifications.Application.Common.Notifications.Services;

public interface ISmsTemplateService
{
    ValueTask<IList<SmsTemplate>> GetByFilterAsync(FilterPagination filterPagination, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<SmsTemplate?> GetByTypeAsync(NotificationTemplateType templateType, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<SmsTemplate> CreateAsync(SmsTemplate smsTemplate, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}