namespace redlist_birds_api.DatabaseContext;

public class IDCreation 
{
    public static string CreateLocId () 
    {
        Random random = new Random ();
        int randomSubId = random.Next(123000000, 160000000);
        string newSubId = $"S{randomSubId}";
        return newSubId;
    }
}