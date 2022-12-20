using redlist_birds_api.Models;

namespace redlist_birds_api.MethodsForRequests;

public interface IGetMethods 
{
   Task<List<RecentObservations>> GetRecentObservations ();
}