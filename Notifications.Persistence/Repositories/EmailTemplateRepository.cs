using System.Linq.Expressions;
using Notifications.Domain.Entities;
using Notifications.Persistence.DataContext;
using Notifications.Persistence.Repositories.Interfaces;

namespace Notifications.Persistence.Repositories;

public class EmailTemplateRepository : EntityRepositoryBase<EmailTemplate, NotificationDbContext>, IEmailTemplateRepository
{
    private IEmailTemplateRepository _emailTemplateRepositoryImplementation;

    public EmailTemplateRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }
    
    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default, bool asNoTacking = false)
    {
        return base.Get(predicate, asNoTacking);
    }

    public ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(emailTemplate, saveChanges, cancellationToken);
    }

}