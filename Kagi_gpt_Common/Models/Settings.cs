
namespace Kagi_gpt_Common.Models;

public record Settings
{
    public PluginConfig[] Plugins { get; set; }
    public string ApiKey { get; set; }
}