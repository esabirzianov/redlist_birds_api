using Newtonsoft.Json;
using redlist_birds_api.Models;
using redlist_birds_api.DatabaseContext;
using redlist_birds_api.ExternalApiSetup;

namespace redlist_birds_api.MethodsForController;

public class BirdInformationRequests : IBirdInformationRequests
{

    public IConfiguration _configuration;
    public IDbConnectionHelper _dbConnectionHelper ;


    public BirdInformationRequests(IConfiguration configuration, IDbConnectionHelper dbConnectionHelper)
    {
        _configuration = configuration;
        _dbConnectionHelper = dbConnectionHelper;
    }
    public async Task<List<RecentObservations>> GetRecentObservations()
    {
        FactoryForQueries factoryForQueries = new FactoryForQueries (_configuration, _dbConnectionHelper);
        IEbirdConnection ebirdConnection = factoryForQueries.eBirdConnection();
        List<RecentObservations> recentObservations = await ebirdConnection.GetRequestEBirdData();
        ISelectQueries selectQueries = factoryForQueries.selectQueries();
        IInsertQueries insertQueries = factoryForQueries.insertQueries();
        Parallel.ForEach<RecentObservations>(recentObservations, async (observation) =>
        {
            await insertQueries.InsertObservationsIntoDb(observation.comName, observation.locName, observation.subId, observation.obsDt, observation.howMany);
        });
        return await selectQueries.SelectObservations();
    }

    public async Task<List<RecentObservations>> GetRecentObservationsByCommonName(string common_name)
    {
        FactoryForQueries factoryForQueries = new FactoryForQueries (_configuration, _dbConnectionHelper);
        ISelectQueries selectQueries = factoryForQueries.selectQueries ();
        return await selectQueries.SelectObservationsByComName(common_name);

    }

    public async Task<RecentObservations> CreateObservation (string _comName, string _locName, int _howMany) 
    {
        FactoryForQueries factoryForQueries = new FactoryForQueries (_configuration, _dbConnectionHelper);
        IInsertQueries insertQueries = factoryForQueries.insertQueries();
        return await insertQueries.InsertSingleObservationInDb( _comName,  _locName,  _howMany);
    }

    public async Task DeleteObservation(string subId)
    {
        FactoryForQueries factoryForQueries = new FactoryForQueries (_configuration, _dbConnectionHelper);
        IDeleteQueries deleteQueries = factoryForQueries.deleteQueries();
        await deleteQueries.DeleteObservation(subId);
    }


    public async Task UpdateObservation(string subId, string comName, int howMany)
    {
        FactoryForQueries factoryForQueries = new FactoryForQueries (_configuration, _dbConnectionHelper);
        IUpdateQueries updateQueries = factoryForQueries.updateQueries();
        await updateQueries.UpdateObservation(subId, comName, howMany);
    }


}
