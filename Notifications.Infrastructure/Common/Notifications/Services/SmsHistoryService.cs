using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Models.Querying;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Application.Common.Querying.Extensions;
using Notifications.Domain.Entities;
using Notifications.Persistence.Repositories.Interfaces;

namespace Notifications.Infrastructure.Common.Notifications.Services;

public class SmsHistoryService : ISmsHistoryService
{
    private readonly ISmsHistoryRepository _smsHistoryRepository;

    public SmsHistoryService(ISmsHistoryRepository smsHistoryRepository)
    {
        _smsHistoryRepository = smsHistoryRepository;
    }

    public async ValueTask<IList<SmsHistory>> GetByFilterAsync(
        FilterPagination filterPagination,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    ) =>
        await _smsHistoryRepository.Get().ApplyPagination(filterPagination)
            .ToListAsync(cancellationToken: cancellationToken);

    public async ValueTask<SmsHistory> CreateAsync(
        SmsHistory smsHistory,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    ) => 
        await _smsHistoryRepository.CreateAsync(smsHistory, saveChanges, cancellationToken);
}