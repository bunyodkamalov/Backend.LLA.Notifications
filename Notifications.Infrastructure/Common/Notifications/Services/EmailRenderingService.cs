using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Notifications.Application.Common.Notifications.Models;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Infrastructure.Common.Settings;


namespace Notifications.Infrastructure.Common.Notifications.Services;

public class EmailRenderingService : IEmailRenderingService
{
    private readonly TemplateRenderingSettings _templateRenderingSettings;

    public EmailRenderingService(IOptions<TemplateRenderingSettings> templateRenderingSettings)
    {
        _templateRenderingSettings = templateRenderingSettings.Value;
    }

    public ValueTask<string> RenderAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        var placeHolderRegex = new Regex(_templateRenderingSettings.PlaceholderRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var placeHolderValueRegex = new Regex(_templateRenderingSettings.PlaceholderValueRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(_templateRenderingSettings.RegexMatchTimeoutInSeconds));

        var matches = placeHolderRegex.Matches(emailMessage.Template.Content);

        var templatePlaceHolders = matches.Select(match =>
            {
                var placeHolder = match.Value;
                var placeHolderValue = placeHolderValueRegex.Match(placeHolder).Groups[1].Value;
                var valid = emailMessage.Variables.TryGetValue(placeHolderValue, out var value);

                return new TemplatePlaceholder
                {
                    PlaceHolder = placeHolder,
                    PlaceHolderValue = placeHolderValue,
                    Value = value,
                    IsValid = valid,
                };
            })
            .ToList();

        ValidatePlaceHolders(templatePlaceHolders);

        var messageBuilder = new StringBuilder(emailMessage.Template.Content);
        templatePlaceHolders.ForEach(placeholder => messageBuilder.Replace(placeholder.PlaceHolder, placeholder.Value));

        var message = messageBuilder.ToString();

        emailMessage.Body = message;
        emailMessage.Subject = emailMessage.Template.Subject;

        return ValueTask.FromResult(message);
    }

    private void ValidatePlaceHolders(IEnumerable<TemplatePlaceholder> templatePlaceholders)
    {
        var missingPlaceHolders = templatePlaceholders.Where(placeholder => !placeholder.IsValid)
            .Select(placeholder => placeholder.Value)
            .ToList();

        if (!missingPlaceHolders.Any()) return;

        var errorMessage = new StringBuilder();
        missingPlaceHolders.ForEach(placeholderValue => errorMessage.Append(placeholderValue).Append(','));

        throw new InvalidOperationException(
            $"Variable for given placeholders is not found - {string.Join(',', missingPlaceHolders)}");
    }
}