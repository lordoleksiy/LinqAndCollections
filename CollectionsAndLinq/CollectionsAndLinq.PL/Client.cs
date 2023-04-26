using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CollectionsAndLinq.BL.Services;

public class Client
{
    private readonly string baseUrl = "https://localhost:7019/api/";
    private readonly HttpClient _client;

    public Client()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }
    public async Task<string> Get(string url)
    {
        var req = await _client.GetAsync(url);
        var resp = await req.Content.ReadAsStringAsync();
        if (!req.IsSuccessStatusCode)
        {
            throw new Exception(resp);
        }

        //var res = JsonConvert.DeserializeObject<T>(resp);

        return resp;
    }
}
