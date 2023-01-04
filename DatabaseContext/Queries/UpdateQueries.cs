using Npgsql;
using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;

public class UpdateQueries : IUpdateQueries

{
    private readonly IDbConnectionHelper _dbConnectionHelper;
    public UpdateQueries(IDbConnectionHelper dbConnectionHelper)
    {
        _dbConnectionHelper = dbConnectionHelper;
    }
    public async Task UpdateObservation(string subId, string comName, int howMany)
    {
        // Update row based on subId data an when return updated row
        var queryText = $"UPDATE ebird_data SET common_name = '{comName}', how_many_observed = '{howMany}' WHERE checklist_id = '{subId}'";

        await using var cmd = new NpgsqlCommand(queryText);

        await _dbConnectionHelper.CreateConnectionIUD(cmd);
    }
}