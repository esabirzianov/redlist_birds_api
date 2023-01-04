namespace redlist_birds_api.Models;

public class RedListData : IRedListData
{
    public string englishName { get; set; }
    public string redListCategory { get; set; }
    public string scientificName { get; set; }
    public string family { get; set; }
}