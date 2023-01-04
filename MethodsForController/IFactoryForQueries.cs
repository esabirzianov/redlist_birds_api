using redlist_birds_api.DatabaseContext;
using redlist_birds_api.ExternalApiSetup;

namespace redlist_birds_api.MethodsForController;

// This class for dependency inversions -> to not create a lot of DI in container and interfaces in constructors.
public class FactoryForQueries 
{
    public IConfiguration _configuration;
    public IDbConnectionHelper _dbConnectionHelper ;

    public FactoryForQueries(IConfiguration configuration, IDbConnectionHelper dbConnectionHelper)
    {
        _configuration = configuration;
        _dbConnectionHelper = dbConnectionHelper;
    }
    public SelectQueries selectQueries () 
    {
        return new SelectQueries(_dbConnectionHelper);
    }

    public InsertQueries insertQueries () 
    {
        return new InsertQueries(_dbConnectionHelper);
    }

    public UpdateQueries updateQueries () 
    {
        return new UpdateQueries(_dbConnectionHelper);
    }
    public DeleteQueries deleteQueries () 
    {
        return new DeleteQueries(_dbConnectionHelper);
    }

    public EbirdConnection eBirdConnection () 
    {
        return new EbirdConnection(_configuration);
    }


}