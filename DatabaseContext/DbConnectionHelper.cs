using Npgsql;
using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;

public class DbConnectionHelper : IDbConnectionHelper
{
    private readonly IConfiguration _configuration;
    public DbConnectionHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    // Create connection and do some code  with operations Insert, Update and Delete
    public async Task CreateConnectionIUD(NpgsqlCommand command)
    {
        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("Ebird_cs"));
        command.Connection = conn;

        // In database we have Constraint to not add duplicate fields, so we don't have information about 
        // Similar checklists from group of people.
        try
        {
            await conn.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await conn.CloseAsync();
        }

        // Here we catch we Exception about Constraint and close the connection, not add anything
        catch
        {
            await conn.CloseAsync();
        }
    }

    public async Task<List<RecentObservations>> SelectRecentObservationsConnection (NpgsqlCommand command)
    {
        List<RecentObservations> result = new ();
        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("Ebird_cs"));
        command.Connection = conn;

        await conn.OpenAsync();
        await using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            // Read data from Database and convert it from NpgsqlDataReader format to List<Recent<Observations>>
            {
                RecentObservations recentObservations = DbDataReader.ReadObservationsFromDb(reader);
                result.Add(recentObservations);
            }
        }
        await conn.CloseAsync();
        return result;
    }

    public async Task<List<RedListData>> SelectRedListDataConnection (NpgsqlCommand command)
    {
        List<RedListData> result = new ();
        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("Ebird_cs"));
        command.Connection = conn;

        await conn.OpenAsync();
        await using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                RedListData redListData = DbDataReader.ReadObservationsFromRedList(reader);
                result.Add(redListData);
            }
        }
        await conn.CloseAsync();
        return result;
    }

    public async Task<List<RedListRecentObservations>> SelectRedListRecentObsConnection(NpgsqlCommand command)
    {
        List<RedListRecentObservations> result = new ();
        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("Ebird_cs"));
        command.Connection = conn;

        await conn.OpenAsync();
        await using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            // Read data from Database and convert it from NpgsqlDataReader format to List<Recent<Observations>>
            {
                RedListRecentObservations redListData = DbDataReader.ReadRedListRecentObs(reader);
                result.Add(redListData);
            }
        }
        await conn.CloseAsync();
        return result;
    }
    

}