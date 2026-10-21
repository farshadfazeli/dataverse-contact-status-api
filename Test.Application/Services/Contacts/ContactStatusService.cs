using Test.Application.Common.Interfaces;
using Test.Domain.CrmEntities.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;

namespace Test.Application.Services.Contacts;

public sealed class ContactStatusService(
    IContactRepository contactRepository,
    ILogger<ContactStatusService> logger) : IContactStatusService
{
    private const int TargetStatusReason = 1;

    public async Task<bool> UpdateStatusReasonByMobilePhoneAsync(
        string mobilePhone,
        CancellationToken cancellationToken)
    {
        var contact = await contactRepository.GetByMobileAsync(mobilePhone, cancellationToken);

        if (contact is null)
        {
            logger.LogWarning("Contact was not found for the supplied mobile phone");
            return false;
        }

        var update = CreateStatusReasonUpdate(contact.Id);
        await contactRepository.UpdateEntityAsync(update, cancellationToken);

        logger.LogInformation(
            "Contact {ContactId} StatusReason changed to {StatusReason}",
            contact.Id,
            TargetStatusReason);

        return true;
    }

    private static ContactEntity CreateStatusReasonUpdate(Guid contactId) =>
        new()
        {
            Id = contactId,
            StatusReason = new OptionSetValue(TargetStatusReason),
        };
}
