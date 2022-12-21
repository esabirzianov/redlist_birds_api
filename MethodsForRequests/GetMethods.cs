using Newtonsoft.Json;
using redlist_birds_api.Interfaces;
using redlist_birds_api.Models;

namespace redlist_birds_api.MethodsForRequests;

public class GetMethods : IGetMethods
{
    private static readonly HttpClient eBirdClient;
    public List<RecentObservations>? result;
    public string url = "";
    public HttpResponseMessage? response;
    public string? responseMessage;

    static GetMethods()
    {
        eBirdClient = new HttpClient();

    }
    public async Task<List<RecentObservations>> GetRecentObservations()
    {
        url = "https://api.ebird.org/v2/data/obs/TR/recent/notable";

        HttpRequestMessage requestMessage = new HttpRequestMessage();
        requestMessage.RequestUri = new Uri(url);
        requestMessage.Headers.Add("x-ebirdapitoken", "jj9fbtjaaahh");

        response = await eBirdClient.SendAsync(requestMessage);
        if (response.IsSuccessStatusCode)
        {
            responseMessage = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<List<RecentObservations>>(responseMessage);

        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
        return result;
    }

    public async Task<List<RecentObservations>> GetRecentObservationsByCommonName(string comName)
    {
        url = "https://api.ebird.org/v2/data/obs/TR/recent/notable";

        HttpRequestMessage requestMessage = new HttpRequestMessage();
        requestMessage.RequestUri = new Uri(url);
        requestMessage.Headers.Add("x-ebirdapitoken", "jj9fbtjaaahh");
        List<RecentObservations> newList = new List<RecentObservations>();

        response = await eBirdClient.SendAsync(requestMessage);
        if (response.IsSuccessStatusCode)
        {
            responseMessage = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<List<RecentObservations>>(responseMessage);
        }
        foreach (RecentObservations value in result)
        {
            if (value.comName == comName)
            {
                newList.Add(value);
            }
        }
        return newList;

    }

    public async Task<List<RecentObservations>> CreateNewObservation (string _comName, string _locName, int _howMany)
    {
        url = "https://api.ebird.org/v2/data/obs/TR/recent/notable";

        HttpRequestMessage requestMessage = new HttpRequestMessage();
        requestMessage.RequestUri = new Uri(url);
        requestMessage.Headers.Add("x-ebirdapitoken", "jj9fbtjaaahh");
        RecentObservations newObservation = new () 
        {
          comName = _comName,
          locName = _locName,
          howMany = _howMany,
          obsDt = Convert.ToString(DateTimeOffset.UtcNow)
        };

        response = await eBirdClient.SendAsync(requestMessage);
        if (response.IsSuccessStatusCode)
        {
            responseMessage = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<List<RecentObservations>>(responseMessage);
        }
        result.Add(newObservation);
        return result;
    }

    public async Task<RecentObservations> UpdateObservation (string obsId, string comName, int howMany)
    // НЕ РАБОТАЕТ!!!!
    {
        url = "https://api.ebird.org/v2/data/obs/TR/recent/notable";

        HttpRequestMessage requestMessage = new HttpRequestMessage();
        requestMessage.RequestUri = new Uri(url);
        requestMessage.Headers.Add("x-ebirdapitoken", "jj9fbtjaaahh");
        RecentObservations changedObservation = new RecentObservations ();

        response = await eBirdClient.SendAsync(requestMessage);
        if (response.IsSuccessStatusCode)
        {
            responseMessage = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<List<RecentObservations>>(responseMessage);
        }

        foreach (RecentObservations value in result) 
        // можно сделать через async parallel (foreach.parallel)
        {
            if (value.locId == obsId) 
            {
                value.comName = comName;
                value.howMany = howMany;
            }

            changedObservation.comName = value.comName;
            changedObservation.obsDt = value.obsDt;
            changedObservation.howMany = value.howMany;
            changedObservation.locId = value.locId;
        }
        return changedObservation;
    }

}