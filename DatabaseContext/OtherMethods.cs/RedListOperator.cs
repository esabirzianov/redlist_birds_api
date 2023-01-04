using Newtonsoft.Json;
using redlist_birds_api.Models;

namespace redlist_birds_api.DatabaseContext;

public class RedListOperator
{
    public static List<RedListData> LoadRedListDataJson()
    {
        using (StreamReader reader = new StreamReader("RedListData.json"))
        {
            string json = reader.ReadToEnd();
            var redListData = JsonConvert.DeserializeObject<List<RedListData>>(json);
            return redListData;
        }

    }
}