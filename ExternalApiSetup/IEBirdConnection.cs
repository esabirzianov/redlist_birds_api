using redlist_birds_api.Models;

namespace redlist_birds_api.ExternalApiSetup;
public interface IEbirdConnection
{
    Task<List<RecentObservations>> GetRequestEBirdData();
}