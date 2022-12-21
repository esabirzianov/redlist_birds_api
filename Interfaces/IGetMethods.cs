using redlist_birds_api.Models;

namespace redlist_birds_api.Interfaces;

public interface IGetMethods 
{
   Task<List<RecentObservations>> GetRecentObservations ();
   Task<List<RecentObservations>> GetRecentObservationsByCommonName(string comName);
   Task<List<RecentObservations>> CreateNewObservation (string _comName, string _locName, int _howMany);
   Task<RecentObservations> UpdateObservation (string obsId, string comName, int howMany);
}