namespace redlist_birds_api.Models;
using redlist_birds_api.Models;

public class RedListSpecies 
{
    public string comName {get; set;}
    // CommonName
    public string groupOfRedList { get; set; }
    // category of red List
    public string populationTrend { get; set; }
    // decreasing or increasing population
}