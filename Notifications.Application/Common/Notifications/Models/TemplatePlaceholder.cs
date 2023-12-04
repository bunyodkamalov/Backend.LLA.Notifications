namespace Notifications.Application.Common.Notifications.Models;

public class TemplatePlaceholder
{
    public string PlaceHolder { get; set; } = default!;

    public string PlaceHolderValue { get; set; } = default!;

    public string? Value { get; set; } = default!;
    
    public bool IsValid { get; set; }
}