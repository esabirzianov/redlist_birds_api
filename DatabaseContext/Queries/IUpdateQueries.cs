namespace redlist_birds_api.DatabaseContext;

public interface IUpdateQueries
{
    Task UpdateObservation(string subId, string comName, int howMany);
    
}