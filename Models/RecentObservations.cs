namespace redlist_birds_api.Models;

public class RecentObservations 
{
    // Common name of specie
    public string comName { get; set; }
    
    // Location 
    public string locName { get; set; }
    
    // Individual Id for checklist
    public string subId {get; set; }

    // Date and time of observation
    public string obsDt {get; set; }
    
    // How many species were observed
    public int howMany { get; set; }

}