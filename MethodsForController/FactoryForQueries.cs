using redlist_birds_api.DatabaseContext;
using redlist_birds_api.ExternalApiSetup;

namespace redlist_birds_api.MethodsForController;

public class FactoryForQueries : IFactoryForQueries
{
    public IConfiguration _configuration;
    public IDbConnectionHelper _dbConnectionHelper;

    public FactoryForQueries(IConfiguration configuration, IDbConnectionHelper dbConnectionHelper)
    {
        _configuration = configuration;
        _dbConnectionHelper = dbConnectionHelper;
    }
    public SelectQueries selectQueries()
    {
        return new SelectQueries(_dbConnectionHelper);
    }

    public InsertQueries insertQueries()
    {
        return new InsertQueries(_dbConnectionHelper);
    }

    public UpdateQueries updateQueries()
    {
        return new UpdateQueries(_dbConnectionHelper);
    }
    public DeleteQueries deleteQueries()
    {
        return new DeleteQueries(_dbConnectionHelper);
    }

    public EbirdConnection eBirdConnection()
    {
        return new EbirdConnection(_configuration);
    }


}