namespace Test.Application.Services.Contacts;

public interface IContactStatusService
{
    Task<bool> UpdateStatusReasonByMobilePhoneAsync(
        string mobilePhone,
        CancellationToken cancellationToken);
}
