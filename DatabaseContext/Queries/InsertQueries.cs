using Npgsql;
using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;

public class InsertQueries 
{
    public IDbConnectionHelper _dbConnectionHelper;

    public InsertQueries( IDbConnectionHelper dbConnectionHelper)
    {
        _dbConnectionHelper = dbConnectionHelper;
    }

    public async Task InsertObservationsIntoDb(string comName, string locName, string subId, string obsDt, int howMany)
    {
        var queryText = "INSERT INTO ebird_data (common_name, location_name, checklist_id, observation_date, how_many_observed) VALUES (@comName, @locName, @subId, @obsDt, @howMany) ";
        await using var cmd = new NpgsqlCommand(queryText)
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
        await _dbConnectionHelper.CreateConnectionIUD(cmd);

    }

    public async Task<RecentObservations> InsertSingleObservationInDb(string _comName, string _locName, int _howMany)
    {
        var newSubId = ChecklistIdCreator.CreateLocId();
        string ObsDt = Convert.ToString(DateTime.UtcNow);
        RecentObservations newObservation = new RecentObservations()
        {
            comName = _comName,
            locName = _locName,
            subId = newSubId,
            obsDt = ObsDt,
            howMany = _howMany

        };

        await using var cmd = new NpgsqlCommand("INSERT INTO  ebird_data (common_name, location_name, checklist_id, observation_date, how_many_observed) VALUES (@comName, @locName, @newSubId, @ObsDt, @howMany) ")
        // In database we have Constraint to not add duplicate fields, so we don't have information about 
        // Similar checklists from group of people.
        {
            Parameters =
            {
                new ("comName", _comName),
                new ("locName", _locName),
                new ("newSubId", newSubId),
                new ("ObsDt", ObsDt),
                new ("howMany", _howMany),

            }
        };

        await _dbConnectionHelper.CreateConnectionIUD(cmd);
        return newObservation;
    }
}