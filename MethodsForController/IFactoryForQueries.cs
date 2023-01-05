using redlist_birds_api.DatabaseContext;
using redlist_birds_api.ExternalApiSetup;

namespace redlist_birds_api.MethodsForController;

public interface IFactoryForQueries
{
    DeleteQueries deleteQueries();
    EbirdConnection eBirdConnection();
    InsertQueries insertQueries();
    SelectQueries selectQueries();
    UpdateQueries updateQueries();
}