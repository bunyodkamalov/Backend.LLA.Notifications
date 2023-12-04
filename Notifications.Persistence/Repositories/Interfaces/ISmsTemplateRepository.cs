using System.Linq.Expressions;
using Notifications.Domain.Entities;

namespace Notifications.Persistence.Repositories.Interfaces;

public interface ISmsTemplateRepository
{
    IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate, bool>>? predicate = default,
        bool asNoTracking = false
    );

    ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsHistory,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );
}