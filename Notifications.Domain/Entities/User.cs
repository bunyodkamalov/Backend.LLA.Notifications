using Notifications.Domain.Common.Entities;
using Notifications.Domain.Enums;

namespace Notifications.Domain.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;
    
    public RoleType Role { get; set; }
    
    public UserSettings UserSettings { get; set; }
}