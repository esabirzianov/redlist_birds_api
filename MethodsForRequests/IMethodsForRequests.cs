using redlist_birds_api.Models;

namespace redlist_birds_api.Interfaces;

public interface IMethodsForRequests
{
    Task<List<RecentObservations>> GetRecentObservations();
    Task DeleteObservation(string subId);

    Task<List<RecentObservations>> GetRecentObservationsByCommonName(string common_name);

    Task<RecentObservations> CreateObservation(string _comName, string _locName, int _howMany);

    Task UpdateObservation(string subId, string comName, int howMany);

}