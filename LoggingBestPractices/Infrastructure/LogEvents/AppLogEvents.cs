namespace LoggingBestPractices.Infrastructure.LogEvents;

public class AppLogEvents
{
    public static AppLogEvent ChangeEmailAddressEvent { get; } = new AppLogEvent(AppLogIds.ChangeEmailAddressId, "User {UserId} updated email address from {OldEmailAddress} to {NewEmailAddress}");
}
