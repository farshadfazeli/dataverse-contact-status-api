using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Data8.PowerPlatform.Dataverse.Client;

namespace Test.Infrastructure.Repositories;

public abstract class BaseRepository(OnPremiseClient client)
{
    protected Task<EntityCollection> RetrieveMultipleAsync(
        QueryBase query,
        CancellationToken cancellationToken) => client.RetrieveMultipleAsync(query, cancellationToken);

    protected Task UpdateAsync(Entity entity, CancellationToken cancellationToken) =>
        client.UpdateAsync(entity, cancellationToken);
}
