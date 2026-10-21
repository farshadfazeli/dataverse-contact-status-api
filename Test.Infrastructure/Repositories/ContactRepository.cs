using System.Security;
using Test.Application.Common.Interfaces;
using Test.Domain.CrmEntities.Models;
using Data8.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;

namespace Test.Infrastructure.Repositories;

public sealed class ContactRepository(OnPremiseClient client)
    : BaseRepository(client), IContactRepository
{
    public async Task<ContactEntity?> GetByMobileAsync(
        string mobilePhone,
        CancellationToken cancellationToken)
    {
        var escapedMobilePhone = SecurityElement.Escape(mobilePhone);

        var fetchXml = $"""
            <fetch version="1.0" output-format="xml-platform" mapping="logical" distinct="false">
              <entity name="contact">
                <attribute name="fullname" />
                <attribute name="telephone1" />
                <attribute name="contactid" />
                <order attribute="fullname" descending="false" />
                <filter type="and">
                  <condition attribute="mobilephone" operator="eq" value="{escapedMobilePhone}" />
                </filter>
              </entity>
            </fetch>
            """;

        var result = await RetrieveMultipleAsync(new FetchExpression(fetchXml), cancellationToken);
        return result.Entities.FirstOrDefault()?.ToEntity<ContactEntity>();
    }

    public Task UpdateEntityAsync(ContactEntity contact, CancellationToken cancellationToken) =>
        UpdateAsync(contact, cancellationToken);
}
