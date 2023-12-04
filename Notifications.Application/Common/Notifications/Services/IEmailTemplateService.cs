using Notifications.Application.Common.Models.Querying;
using Notifications.Domain.Entities;
using Notifications.Domain.Enums;

namespace Notifications.Application.Common.Notifications.Services;

public interface IEmailTemplateService
{
    ValueTask<IList<EmailTemplate>> GetByFilterAsync(FilterPagination filterPagination, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<EmailTemplate?> GetByTypeAsync(NotificationTemplateType templateType, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}