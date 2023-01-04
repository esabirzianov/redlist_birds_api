using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;
public interface IInsertQueries

{
    Task InsertObservationsIntoDb(string comName, string locName, string subId, string obsDt, int howMany);
    Task<RecentObservations> InsertSingleObservationInDb(string _comName, string _locName, int _howMany);
}