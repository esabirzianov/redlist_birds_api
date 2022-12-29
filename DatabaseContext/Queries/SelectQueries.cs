using Npgsql;
using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;



public class SelectQueries 
{
    private readonly IDbConnectionHelper _dbConnectionHelper;

    public SelectQueries(IDbConnectionHelper dbConnectionHelper)
    {
        _dbConnectionHelper = dbConnectionHelper;
    }
    public async Task<List<RecentObservations>> SelectObservations()
    {
        var queryText = "SELECT * FROM ebird_data ORDER BY observation_date";
        await using var cmd = new NpgsqlCommand(queryText);
        return await _dbConnectionHelper.ConnectionWithSelect(cmd);
    }

      public async Task<List<RecentObservations>> SelectObservationsByComName(string comName)
    {
        var queryText = $"SELECT * FROM ebird_data WHERE common_name = '{comName}' ORDER BY observation_date";
        await using var cmd = new NpgsqlCommand(queryText);
        return await _dbConnectionHelper.ConnectionWithSelect(cmd);
    }

}

