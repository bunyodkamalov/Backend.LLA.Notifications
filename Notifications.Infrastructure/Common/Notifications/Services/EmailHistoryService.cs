using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Models.Querying;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Application.Common.Querying.Extensions;
using Notifications.Domain.Entities;
using Notifications.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Common.Notifications.Services;

public class EmailHistoryService : IEmailHistoryService
{
    private readonly IEmailHistoryRepository _emailHistoryRepository;

    public EmailHistoryService(IEmailHistoryRepository emailHistoryRepository)
    {
        _emailHistoryRepository = emailHistoryRepository;
    }

    public async Task<List<EmailHistory>> GetByFilterAsync(
        FilterPagination filterPagination,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) =>
        await _emailHistoryRepository.Get().ApplyPagination(filterPagination)
            .ToListAsync(cancellationToken: cancellationToken);


    public async ValueTask<EmailHistory> CreateAsync(
        EmailHistory emailHistory,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    ) =>
        await _emailHistoryRepository.CreateAsync(emailHistory, saveChanges, cancellationToken);
}