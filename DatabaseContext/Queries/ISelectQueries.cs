using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;

public interface ISelectQueries
{
    Task<List<RecentObservations>> SelectObservations();
    Task<List<RecentObservations>> SelectObservationsByComName(string comName);
    Task<List<RedListData>> SelectRedListData();
    Task<List<RedListRecentObservations>> SelectRedListRecentObservations();
}