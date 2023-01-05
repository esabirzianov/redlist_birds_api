using Newtonsoft.Json;
using redlist_birds_api.Models;
using redlist_birds_api.DatabaseContext;
using redlist_birds_api.ExternalApiSetup;

namespace redlist_birds_api.MethodsForController;

public class BirdInformationRequests : IBirdInformationRequests
{

    public IConfiguration _configuration;
    public IDbConnectionHelper _dbConnectionHelper ;

    public IFactoryForQueries _factoryForQueries;
    public BirdInformationRequests(IConfiguration configuration, IDbConnectionHelper dbConnectionHelper, IFactoryForQueries factoryForQueries)
    {
        _configuration = configuration;
        _dbConnectionHelper = dbConnectionHelper;
        _factoryForQueries = factoryForQueries;
    }
    public async Task<List<RecentObservations>> GetRecentObservations()
    {
        IEbirdConnection ebirdConnection = _factoryForQueries.eBirdConnection();
        List<RecentObservations> recentObservations = await ebirdConnection.GetRequestEBirdData();
        ISelectQueries selectQueries = _factoryForQueries.selectQueries();
        IInsertQueries insertQueries = _factoryForQueries.insertQueries();
        Parallel.ForEach<RecentObservations>(recentObservations, async (observation) =>
        {
            await insertQueries.InsertObservationsIntoDb(observation.comName, observation.locName, observation.subId, observation.obsDt, observation.howMany);
        });
        return await selectQueries.SelectObservations();
    }

    public async Task<List<RecentObservations>> GetRecentObservationsByCommonName(string common_name)
    {
        ISelectQueries selectQueries = _factoryForQueries.selectQueries ();
        return await selectQueries.SelectObservationsByComName(common_name);

    }

    public async Task<List<RecentObservations>> GetRecentObservationsBySubId(string subId)
    {
        ISelectQueries selectQueries = _factoryForQueries.selectQueries ();
        return await selectQueries.SelectObservationsBySubId(subId);

    }
    public async Task<RecentObservations> CreateObservation (string _comName, string _locName, int _howMany) 
    {
        IInsertQueries insertQueries = _factoryForQueries.insertQueries();
        return await insertQueries.InsertSingleObservationInDb( _comName,  _locName,  _howMany);
    }

    public async Task DeleteObservation(string subId)
    {
        IDeleteQueries deleteQueries = _factoryForQueries.deleteQueries();
        await deleteQueries.DeleteObservation(subId);
    }


    public async Task UpdateObservation(string subId, string comName, int howMany)
    {
        IUpdateQueries updateQueries = _factoryForQueries.updateQueries();
        await updateQueries.UpdateObservation(subId, comName, howMany);
    }


}
