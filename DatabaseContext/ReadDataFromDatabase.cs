using Npgsql;
using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;

public class ReadDataFromDatabase 
{
    public static RecentObservations ReadObservationsFromDb (NpgsqlDataReader reader) 
    {
        string _comName = reader["common_name"] as string;
        string _locName = reader["location_name"] as string;
        string _subId = reader["checklist_id"] as string;
        string _obsDt = reader["observation_date"] as string;
        int? _howMany = reader["how_many_observed"] as int?;
        RecentObservations result = new RecentObservations 
        {
            comName = _comName,
            locName = _locName,
            subId = _subId,
            obsDt = _obsDt,
            howMany = _howMany.Value
        };
        return result;
    }

}

