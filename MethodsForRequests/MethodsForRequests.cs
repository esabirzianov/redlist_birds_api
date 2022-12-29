using Newtonsoft.Json;
using redlist_birds_api.Interfaces;
using redlist_birds_api.Models;
using redlist_birds_api.DatabaseContext;
using redlist_birds_api.ExternalApiSetup;

namespace redlist_birds_api.MethodsForRequests;

public class MethodsForRequests : IMethodsForRequests
{

    public IConfiguration _configuration;
    public IDbConnectionHelper _dbConnectionHelper ;


    public MethodsForRequests(IConfiguration configuration, IDbConnectionHelper dbConnectionHelper)
    {
        _configuration = configuration;
        _dbConnectionHelper = dbConnectionHelper;
    }
    public async Task<List<RecentObservations>> GetRecentObservations()
    {
        EbirdConnection ebirdConnection = new EbirdConnection(_configuration);
        List<RecentObservations> recentObservations = await ebirdConnection.GetRequestEBirdData();
        SelectQueries selectQueries = new SelectQueries (_dbConnectionHelper);
        InsertQueries insertQueries = new InsertQueries (_dbConnectionHelper);
        Parallel.ForEach<RecentObservations>(recentObservations, async (observation) =>
        {
            await insertQueries.InsertObservationsIntoDb(observation.comName, observation.locName, observation.subId, observation.obsDt, observation.howMany);
        });
        return await selectQueries.SelectObservations();
    }

    public async Task<List<RecentObservations>> GetRecentObservationsByCommonName(string common_name)
    {
        SelectQueries selectQueries = new SelectQueries (_dbConnectionHelper);
        return await selectQueries.SelectObservationsByComName(common_name);

    }

    public async Task<RecentObservations> CreateObservation (string _comName, string _locName, int _howMany) 
    {
        InsertQueries insertQueries = new InsertQueries (_dbConnectionHelper);
        return await insertQueries.InsertSingleObservationInDb( _comName,  _locName,  _howMany);
    }

    public async Task DeleteObservation(string subId)
    {
        DeleteQueries deleteQueries = new DeleteQueries( _dbConnectionHelper);
        await deleteQueries.DeleteObservation(subId);
    }


    public async Task UpdateObservation(string subId, string comName, int howMany)
    {
        UpdateQueries updateQueries = new UpdateQueries(_dbConnectionHelper);
         await updateQueries.UpdateObservation(subId, comName, howMany);
    }


}
