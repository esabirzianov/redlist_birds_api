using Npgsql;
using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;

public class DatabaseConnection
{
    public IConfiguration _configuration;

    public DatabaseConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task InsertObservationsIntoDb(string comName, string locName, string subId, string obsDt, int howMany)
    {

        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("Ebird_cs"));
        try
        {
            var queryText = "INSERT INTO  ebird_data (common_name, location_name, checklist_id, observation_date, how_many_observed) VALUES (@comName, @locName, @subId, @obsDt, @howMany) ";
            await using var cmd = new NpgsqlCommand(queryText, conn)
            // In database we have Constraint to not add duplicate fields, so we don't have information about 
            // Similar checklists from group of people.
            {
                Parameters =
                {

                    new ("comName", comName),
                    new ("locName", locName),
                    new ("subId", subId),
                    new ("obsDt", obsDt),
                    new ("howMany", howMany),

                },

            };
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();

        }
        catch
        // Here we catch we Exception about Constraint and close the connection, not add anything
        {
            await conn.CloseAsync();
        }
    }

    public async Task<RecentObservations> InsertSingleObservationInDb(string _comName, string _locName, int _howMany)
    {
        var newSubId = IDCreation.CreateLocId();
        string ObsDt = Convert.ToString(DateTime.UtcNow);
        RecentObservations newObservation = new RecentObservations()
        {
            comName = _comName,
            locName = _locName,
            subId = newSubId,
            obsDt = ObsDt,
            howMany = _howMany

        };
        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("Ebird_cs"));
        try
        {
            await using var cmd = new NpgsqlCommand("INSERT INTO  ebird_data (common_name, location_name, checklist_id, observation_date, how_many_observed) VALUES (@comName, @locName, @newSubId, @ObsDt, @howMany) ", conn)
            // In database we have Constraint to not add duplicate fields, so we don't have information about 
            // Similar checklists from group of people.
            {
                Parameters =
            {
                new ("comName", _comName),
                new ("locName", _locName),
                new ("newSubId", newSubId),
                new ("ObsDt", ObsDt),
                new ("howMany", _comName),

            }
            };
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();

        }
        catch
        // Here we catch we Exception about Constraint and close the connection, not add anything
        {
            await conn.CloseAsync();
        }
        return newObservation;
    }

    public async Task<List<RecentObservations>> SelectObservations()
    {
        List<RecentObservations> result = new List<RecentObservations>();
        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("Ebird_cs"));
        var queryText = "SELECT * FROM ebird_data ORDER BY observation_date";
        await using var cmd = new NpgsqlCommand(queryText, conn);
        await conn.OpenAsync();
        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            // Read data from Database and convert it from NpgsqlDataReader format to List<Recent<Observations>>
            {
                RecentObservations recentObservations = ReadDataFromDatabase.ReadObservationsFromDb(reader);
                result.Add(recentObservations);
            }
        }
        await conn.CloseAsync();
        return result;
    }

    public async Task<List<RecentObservations>> SelectObservationsByComName(string comName)
    {
        List<RecentObservations> result = new List<RecentObservations>();
        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("Ebird_cs"));
        var queryText = $"SELECT * FROM ebird_data WHERE common_name = '{comName}' ORDER BY observation_date";
        await using var cmd = new NpgsqlCommand(queryText, conn);
        await conn.OpenAsync();
        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {

            while (await reader.ReadAsync())
            // Read data from Database and convert it from NpgsqlDataReader format to List<Recent<Observations>>
            {
                RecentObservations recentObservations = ReadDataFromDatabase.ReadObservationsFromDb(reader);
                result.Add(recentObservations);
            }

        }
        await conn.CloseAsync();
        return result;
    }

    public async Task<RecentObservations> SelectObservationBySubid(string subId)
    {
        RecentObservations recentObservations = new RecentObservations ();
        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("Ebird_cs"));
        var queryText = $"SELECT * FROM ebird_data WHERE checklist_id = '{subId}' ORDER BY observation_date";
        await using var cmd = new NpgsqlCommand(queryText, conn);
        await conn.OpenAsync();
        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {

            while (await reader.ReadAsync())
            // Read data from Database and convert it from NpgsqlDataReader format to RecentObservations
            {
                recentObservations = ReadDataFromDatabase.ReadObservationsFromDb(reader);
            }
            
        }
        await conn.CloseAsync();
        return recentObservations;
        
    }
    public async Task<RecentObservations> UpdateObservation(string subId, string comName, int howMany)
    {
        RecentObservations updateObservation = new RecentObservations ();
        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("Ebird_cs"));
        
        // Update row based on subId data an when return updated row
        var queryText = $"UPDATE ebird_data SET common_name = '{comName}', how_many_observed = '{howMany}' WHERE checklist_id = '{subId}' RETURNING * ";

        await using var cmd = new NpgsqlCommand(queryText, conn);
        await conn.OpenAsync();
        
        await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
        {

            while (await reader.ReadAsync())
            // Read data from Database and convert it from NpgsqlDataReader format to RecentObservations
            {
                updateObservation = ReadDataFromDatabase.ReadObservationsFromDb(reader);
            }
        }
        await conn.CloseAsync();
        return updateObservation;
        
    }

    public async Task DeleteObservation(string subId)
    {
        await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("Ebird_cs"));
        var queryText = $"DELETE FROM ebird_data WHERE checklist_id = '{subId}' ";
        await using var cmd = new NpgsqlCommand(queryText, conn);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
        await conn.CloseAsync();
    }
}




