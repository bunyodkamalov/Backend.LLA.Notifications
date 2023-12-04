using System.Linq.Expressions;
using Notifications.Domain.Common.Exeptions;
using Notifications.Domain.Entities;

namespace Notifications.Persistence.Repositories.Interfaces;

public interface IEmailTemplateRepository
{
    IQueryable<EmailTemplate> Get(
        Expression<Func<EmailTemplate, bool>>? predicate = default,
        bool asNoTacking = false
    );

    ValueTask<EmailTemplate> CreateAsync(
        EmailTemplate emailTemplate, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default
        );
    
}