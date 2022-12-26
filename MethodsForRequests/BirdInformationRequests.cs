using Newtonsoft.Json;
using redlist_birds_api.Interfaces;
using redlist_birds_api.Models;
using redlist_birds_api.DatabaseContext;
using redlist_birds_api.ExternalApiSetup;

namespace redlist_birds_api.MethodsForRequests;

public class BirdInformationRequests : IBirdInformationRequests
{

    public IConfiguration _configuration;

    public BirdInformationRequests(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<List<RecentObservations>> GetRecentObservations()
    {
        EbirdConnection ebirdConnection = new EbirdConnection(_configuration);
        DatabaseConnection databaseConnection = new DatabaseConnection(_configuration);
        List<RecentObservations> recentObservations = await ebirdConnection.GetRequestEBirdData();
        Parallel.ForEach<RecentObservations>(recentObservations, async (observation) =>
        {
            await databaseConnection.InsertObservationsIntoDb(observation.comName, observation.locName, observation.subId, observation.obsDt, observation.howMany);
        });
        return await databaseConnection.SelectObservations();
    }

    public async Task<List<RecentObservations>> GetRecentObservationsByCommonName(string comName)
    {
        DatabaseConnection databaseConnection = new DatabaseConnection(_configuration);
        return await databaseConnection.SelectObservationsByComName(comName);
    }


    public async Task<RecentObservations> CreateNewObservation(string _comName, string _locName, int _howMany)
    {
        DatabaseConnection databaseConnection = new DatabaseConnection(_configuration);
        return await databaseConnection.InsertSingleObservationInDb(_comName, _locName, _howMany);
    }

    public async Task DeleteObservation(string subId)
    {
        DatabaseConnection databaseConnection = new DatabaseConnection(_configuration);
        await databaseConnection.DeleteObservation(subId);
    }


    public async Task<RecentObservations> UpdateObservation(string subId, string comName, int howMany)
    {
        DatabaseConnection databaseConnection = new DatabaseConnection(_configuration);
        return await databaseConnection.UpdateObservation(subId, comName, howMany);
        
    }


}
