namespace redlist_birds_api.DatabaseContext;
public interface IDeleteQueries
{
    Task DeleteObservation(string subId);
}