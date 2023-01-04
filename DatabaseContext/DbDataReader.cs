using Npgsql;
using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;

public class DbDataReader 
{
    public static RecentObservations ReadObservationsFromDb (NpgsqlDataReader reader) 
    {
        string _comName = reader["common_name"] as string;
        string _locName = reader["location_name"] as string;
        string _subId = reader["checklist_id"] as string;
        string _obsDt = reader["observation_date"] as string;
        int? _howMany = reader["how_many_observed"] as int?;
        ICommonRecentObs result = new RecentObservations 
        {
            comName = _comName,
            locName = _locName,
            subId = _subId,
            obsDt = _obsDt,
            howMany = _howMany.Value
        };
        return (RecentObservations)result;
    }

    public static RedListData ReadObservationsFromRedList (NpgsqlDataReader reader) 
    {
        string english_name = reader["english_name"] as string;
        string red_list_category = reader["red_list_category"] as string;
        string scientific_name = reader["scientific_name"] as string;
        string family_of_species = reader["family_of_species"] as string;
        IRedListData redListData = new RedListData 
        {
            englishName = english_name,
            redListCategory = red_list_category,
            scientificName = scientific_name,
            family = family_of_species
        };
        return (RedListData)redListData;
    }

    public static RedListRecentObservations ReadRedListRecentObs (NpgsqlDataReader reader) 
    {
        string common_name = reader["common_name"] as string;
        string location_name = reader["location_name"] as string;
        string observation_date = reader["observation_date"] as string;
        int? how_many_observed = reader["how_many_observed"] as int?;
        string red_list_category = reader["red_list_category"] as string;
        string scientific_name = reader["scientific_name"] as string;
        string family = reader["family_of_species"] as string;

        IRedListRecentObservations result = new RedListRecentObservations 
        {
            comName = common_name,
            locName = location_name,
            obsDt = observation_date,
            howMany = how_many_observed.Value,
            redListCategory = red_list_category,
            scientificName = scientific_name,
            family = family
        };
        return (RedListRecentObservations)result;
    }

}

