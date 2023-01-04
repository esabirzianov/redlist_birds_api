namespace redlist_birds_api.Models;

public interface ICommonRecentObs 
{
    string comName { get; set; }
    string locName { get; set; }
    string obsDt { get; set; }
    int howMany { get; set; }
}