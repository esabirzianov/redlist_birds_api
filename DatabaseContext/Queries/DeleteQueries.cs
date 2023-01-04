using Npgsql;

namespace redlist_birds_api.DatabaseContext;

public class DeleteQueries : IDeleteQueries
{
    private readonly IDbConnectionHelper _dbConnectionHelper;

    public DeleteQueries(IDbConnectionHelper dbConnectionHelper)
    {
        _dbConnectionHelper = dbConnectionHelper;
    }

    public async Task DeleteObservation(string subId)
    {
        var queryText = $"DELETE FROM ebird_data WHERE checklist_id = '{subId}' ";
        await using var cmd = new NpgsqlCommand(queryText);
        await _dbConnectionHelper.CreateConnectionIUD(cmd);
    }

}