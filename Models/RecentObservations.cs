using Newtonsoft.Json;

namespace redlist_birds_api.Models;

public class RecentObservations 
{
    
    public string comName { get; set; }
    // Common name of specie
    
    public string locName { get; set; }
    
    

    public string obsDt {get; set; }
    // Location where found
   
    public int howMany { get; set; }

    public string locId { get; set; }
    // Quantity of species
}