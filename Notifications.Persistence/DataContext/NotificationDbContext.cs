using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;

namespace Notifications.Persistence.DataContext;

public class NotificationDbContext : DbContext
{
    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

    public DbSet<SmsTemplate> SmsTemplates => Set<SmsTemplate>();

    public DbSet<EmailHistory> EmailHistories => Set<EmailHistory>();

    public DbSet<SmsHistory> SmsHistories => Set<SmsHistory>();

    public DbSet<User> Users => Set<User>();

    public DbSet<UserSettings> UserSettings => Set<UserSettings>();

    public NotificationDbContext(DbContextOptions<NotificationDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
    }
}