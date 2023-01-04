namespace redlist_birds_api.Models;

public interface IRedListData
{
    string redListCategory { get; set; }
    string scientificName { get; set; }
    string family { get; set; }
}



