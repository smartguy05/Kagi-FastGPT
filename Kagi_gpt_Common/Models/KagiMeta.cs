using Newtonsoft.Json;

namespace Kagi_gpt_Common.Models;

public record KagiMeta
{
    public string Id { get; set; }
    public string Node { get; set; }
    public int Ms { get; set; }
    [JsonProperty("api_balance")]
    public double ApiBalance { get; set; }
}