namespace redlist_birds_api.ExternalApiConnection;

public class ApiModel
{
    public string apiKey { get; set; }
    public string apiValue { get; set; }
    public ApiModel(string _ApiKey, string _apiValue)
    {
        apiKey = _ApiKey;
        apiValue = _ApiKey;
    }
}