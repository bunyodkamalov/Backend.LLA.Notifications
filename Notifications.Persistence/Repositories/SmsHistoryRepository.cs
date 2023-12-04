using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;
using Notifications.Persistence.DataContext;
using Notifications.Persistence.Repositories.Interfaces;

namespace Notifications.Persistence.Repositories;

public class SmsHistoryRepository : EntityRepositoryBase<SmsHistory, NotificationDbContext>, ISmsHistoryRepository
{
    public SmsHistoryRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<SmsHistory> Get(Expression<Func<SmsHistory, bool>>? predicate = default,
        bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public async ValueTask<SmsHistory> CreateAsync(SmsHistory smsHistory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (smsHistory.Template is not null)
            DbContext.Entry(smsHistory.Template).State = EntityState.Unchanged;
        
        var createdHistory = await base.CreateAsync(smsHistory, saveChanges, cancellationToken);
        
        if (smsHistory.Template is not null)
            DbContext.Entry(smsHistory.Template).State = EntityState.Detached;

        return createdHistory;
    }
}