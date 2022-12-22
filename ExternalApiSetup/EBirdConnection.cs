using Newtonsoft.Json;
using redlist_birds_api.Models;

namespace redlist_birds_api.ExternalApiSetup;

public class EbirdConnection
{
    List<RecentObservations> result ;
    public IConfiguration _configuration;
    
    public EbirdConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<RecentObservations>> GetRequestEBirdData()
    {
        // Get the URL from appsetings.json to connect with EBird Api
        var url = _configuration.GetValue<string>("EBirdInformation:EbirdURL");

        HttpRequestMessage requestMessage = new HttpRequestMessage();
        requestMessage.RequestUri = new Uri(url);

        // Add individual Api Key and Value to request headers to accept Api
        requestMessage.Headers.Add(_configuration.GetValue<string>("EBirdInformation:EBirdKey"), _configuration.GetValue<string>("EBirdInformation:EBirdValue"));

        HttpResponseMessage response = new HttpResponseMessage ();
        HttpClient eBirdClient = new HttpClient ();
        response = await eBirdClient.SendAsync(requestMessage);

        // Check if the responce ok -> do serialization to List<RecentObservations> and return it

        if (response.IsSuccessStatusCode)
        {
            var responseMessage = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<List<RecentObservations>>(responseMessage);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
        return result;
    }

}