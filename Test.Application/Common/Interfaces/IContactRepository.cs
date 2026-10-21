using Test.Domain.CrmEntities.Models;

namespace Test.Application.Common.Interfaces;

public interface IContactRepository
{
    Task<ContactEntity?> GetByMobileAsync(string mobilePhone, CancellationToken cancellationToken);
    Task UpdateEntityAsync(ContactEntity contact, CancellationToken cancellationToken);
}
