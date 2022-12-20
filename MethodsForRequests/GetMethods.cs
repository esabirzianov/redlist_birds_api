using System.Text.Json;

using redlist_birds_api.Models;

namespace redlist_birds_api.MethodsForRequests;

public class GetMethods : IGetMethods
{
    private static readonly HttpClient eBirdClient; 
    public List<RecentObservations> result;
    public string url = "";
    public HttpResponseMessage response;
    public string responseMessage;
    
    static GetMethods ()
    {
        eBirdClient = new HttpClient ();
        
    }   
    public async Task<List<RecentObservations>> GetRecentObservations () 
    {
        url = "https://api.ebird.org/v2/data/obs/TR/recent/notable";
        result = new List<RecentObservations>();
        HttpRequestMessage requestMessage = new HttpRequestMessage();
        requestMessage.RequestUri = new Uri (url);
        requestMessage.Headers.Add("x-ebirdapitoken", "jj9fbtjaaahh");

        response = await eBirdClient.SendAsync(requestMessage);
        if (response.IsSuccessStatusCode) 
        {
            responseMessage = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<List<RecentObservations>>(responseMessage);
        }
        return result;
    }
    
}