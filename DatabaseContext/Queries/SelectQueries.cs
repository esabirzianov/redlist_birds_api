using Npgsql;
using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;

public class SelectQueries : ISelectQueries
{
    private readonly IDbConnectionHelper _dbConnectionHelper;

    public SelectQueries(IDbConnectionHelper dbConnectionHelper)
    {
        _dbConnectionHelper = dbConnectionHelper;
    }
    public async Task<List<RecentObservations>> SelectObservations()
    {
        var queryText = "SELECT * FROM ebird_data ORDER BY observation_date DESC";
        await using var cmd = new NpgsqlCommand(queryText);
        return await _dbConnectionHelper.SelectRecentObservationsConnection(cmd);
    }

    public async Task<List<RecentObservations>> SelectObservationsByComName(string comName)
    {
        var queryText = $"SELECT * FROM ebird_data WHERE common_name = '{comName}' ORDER BY observation_date DESC";
        await using var cmd = new NpgsqlCommand(queryText);
        return await _dbConnectionHelper.SelectRecentObservationsConnection(cmd);
    }

    public async Task<List<RedListData>> SelectRedListData()
    {
        var queryText = "SELECT * FROM redlistdata";
        await using var cmd = new NpgsqlCommand(queryText);
        return await _dbConnectionHelper.SelectRedListDataConnection(cmd);
    }


    public async Task<List<RedListRecentObservations>> SelectRedListRecentObservations()
    {
        var queryText = @"SELECT common_name, location_name, observation_date, how_many_observed,
        family_of_species, red_list_category, scientific_name 
        FROM redlistdata 
        INNER JOIN ebird_data 
        ON redlistdata.english_name = ebird_data.common_name ";

        await using var cmd = new NpgsqlCommand(queryText);
        return await _dbConnectionHelper.SelectRedListRecentObsConnection(cmd);
    }
}

