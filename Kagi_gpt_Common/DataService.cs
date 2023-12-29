using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Kagi_gpt_Common.Models;
using Newtonsoft.Json;

namespace Kagi_gpt_Common;

public class DataService
{
    public static async Task<KagiResponse> SendRequest(string content)
    {
        var http = new HttpClient();
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", Config.ApiKey);
        
        var body = new
        {
            query = content
        };
        var request = new HttpRequestMessage(HttpMethod.Post, "https://kagi.com/api/v0/fastgpt");
        request.Content = JsonContent.Create(body);
        request.Content.Headers.Remove("Content-Type");
        request.Content.Headers.Add("Content-Type", "application/json");
        
        var response = await http.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<KagiResponse>();
        }

        return null;
    }
}