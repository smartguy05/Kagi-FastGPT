using System.Reflection;
using Kagi_gpt_Common.Models;
using Microsoft.Extensions.Configuration;

namespace Kagi_gpt_Common;

public static class Config
{
    private static readonly Settings Settings;

    public static PluginConfig[] Plugins => Settings?.Plugins;
    public static string ApiKey => Settings?.ApiKey;
    
    static Config()
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets(Assembly.GetEntryAssembly(), true);
        var config = new Settings();
        configBuilder.Build().Bind(config);
        Settings = config;
    }
}