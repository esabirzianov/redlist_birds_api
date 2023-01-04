using Npgsql;
using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;

public interface IDbConnectionHelper
{
    Task CreateConnectionIUD(NpgsqlCommand command);
    Task<List<RecentObservations>> SelectRecentObservationsConnection(NpgsqlCommand command);
    Task<List<RedListData>> SelectRedListDataConnection(NpgsqlCommand command);
     Task<List<RedListRecentObservations>> SelectRedListRecentObsConnection(NpgsqlCommand command);
}
