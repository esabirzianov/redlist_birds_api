namespace redlist_birds_api.Models;

public class RedListRecentObservations : IRedListRecentObservations
{
    public string comName { get; set; }
    public string locName { get; set; }
    public string obsDt { get; set; }
    public int howMany { get; set; }
    public string redListCategory { get; set; }
    public string scientificName { get; set; }
    public string family { get; set; }
}