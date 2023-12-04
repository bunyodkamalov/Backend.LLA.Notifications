using System.Linq.Expressions;
using Notifications.Domain.Entities;
using Notifications.Persistence.DataContext;
using Notifications.Persistence.Repositories.Interfaces;

namespace Notifications.Persistence.Repositories;

public class SmsTemplateRepository : EntityRepositoryBase<SmsTemplate, NotificationDbContext>, ISmsTemplateRepository
{
    public SmsTemplateRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<SmsTemplate> Get(Expression<Func<SmsTemplate, bool>>? predicate = default,
        bool asNoTracking = false
        ) => 
            base.Get(predicate, asNoTracking);


    public ValueTask<SmsTemplate> CreateAsync(SmsTemplate smsHistory, bool saveChanges = true,
        CancellationToken cancellationToken = default
        ) => 
            base.CreateAsync(smsHistory, saveChanges, cancellationToken);


}