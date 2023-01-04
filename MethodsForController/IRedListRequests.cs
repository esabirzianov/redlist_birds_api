using redlist_birds_api.Models;

namespace redlist_birds_api.MethodsForController;

public interface IRedListRequests
{
    Task<List<RedListRecentObservations>> GetRedListRecentObservationsAsync();
    Task <List<RedListData>> GetRedListDataAsync () ;
}