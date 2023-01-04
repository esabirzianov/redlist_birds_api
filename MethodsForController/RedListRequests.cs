using redlist_birds_api.DatabaseContext;
using redlist_birds_api.Models;

namespace redlist_birds_api.MethodsForController;

public class RedListRequests : IRedListRequests
{
    public IDbConnectionHelper _dbConnectionHelper;
    public IConfiguration _configuration;


    public RedListRequests(IDbConnectionHelper dbConnectionHelper, IConfiguration configuration)
    {
        _dbConnectionHelper = dbConnectionHelper;
        _configuration = configuration;
    }
    
    public async Task <List<RedListData>> GetRedListDataAsync () 
    {
        FactoryForQueries factoryForQueries = new FactoryForQueries (_configuration, _dbConnectionHelper);
        ISelectQueries selectQueries = factoryForQueries.selectQueries();
        return await selectQueries.SelectRedListData();
    }
    

    public async Task<List<RedListRecentObservations>> GetRedListRecentObservationsAsync()
    {
        FactoryForQueries factoryForQueries = new FactoryForQueries (_configuration, _dbConnectionHelper);
        ISelectQueries selectQueries = factoryForQueries.selectQueries();
        return await selectQueries.SelectRedListRecentObservations();
    }
}