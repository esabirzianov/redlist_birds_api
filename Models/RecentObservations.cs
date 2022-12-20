namespace redlist_birds_api.Models;

public class RecentObservations 
{
    public string comName { get; set; }
    // Common name of specie
    public string LocName { get; set; }
    // Location where found
    
    public int HowMany { get; set; }
    // Quantity of species
}