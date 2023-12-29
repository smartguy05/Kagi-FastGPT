using System.Reflection;
using Kagi_gpt_Common;
using Kagi_gpt_Common.Interfaces;

namespace Kagi_gpt;
public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Enter your prompt:");
                var data = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(data))
                {
                    Console.WriteLine("No prompt detected, exiting...");
                    Console.ReadLine();
                    return;
                }

                args = new[] { data };
            }

            var commandName = args.FirstOrDefault(f => f.StartsWith("--"));
            // todo: better way to get plugin flags
            var commandParams = args.Length == 1
                ? args
                : args.Skip(1).ToArray();

            var plugin = Config.Plugins.FirstOrDefault(f => string.Equals(f.Name, commandName, StringComparison.InvariantCultureIgnoreCase));

            if (plugin != null)
            {
                Console.WriteLine($"-- {plugin.Name} --");

                var pluginAssembly = LoadPlugin(plugin.Directory);
                var commands = CreateCommands(pluginAssembly);
                
                foreach (var command in commands)
                {
                    command?.Execute(commandParams);   
                }

                Console.WriteLine();
            }
            else
            {
                var data = string.Join(" ", commandParams); 
                var result = DataService.SendRequest(data).Result;
                if (result.Error == null && result.Data != null)
                {
                    Console.Write(result?.Data?.Output);
                    Console.WriteLine();
                    if (result.Data.References.Any())
                    {
                        Console.WriteLine();
                        Console.WriteLine("References:");
                        foreach (var reference in result.Data.References)
                        {
                            Console.WriteLine(reference.Title);
                            Console.Write(reference.Snippet);
                            Console.WriteLine();
                            Console.WriteLine(reference.Url);
                            Console.WriteLine();
                        }   
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("An error occurred while processing your request.");
                    foreach (var error in result.Error)
                    {
                        Console.WriteLine($"Message: {error.Msg}");
                        Console.WriteLine($"Code: {error.Code}");
                        Console.WriteLine($"Ref: {error.Ref}");
                        Console.WriteLine();
                    }
                }
                
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    // private static PluginConfig RequestedPlugins(params string[] args)
    // {
    //     if (!args.Any(a => a.StartsWith("--")))
    //     {
    //         return null;
    //     }
    //     
    //     var plugin = args.FirstOrDefault(f => f.StartsWith("--"));
    //     var index = Array.IndexOf(args, plugin);
    //     if (index == 0)
    //     {
    //         
    //     }
    // }
    
    private static Assembly LoadPlugin(string relativePath)
    {
        // Navigate up to the solution root
        var root = Path.GetFullPath(Path.Combine(
            Path.GetDirectoryName(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

        var pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
        Console.WriteLine($"Loading commands from: {pluginLocation}");
        var loadContext = new PluginLoadContext(pluginLocation);
        return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
    }

    private static IEnumerable<ICommand> CreateCommands(Assembly assembly)
    {
        var count = 0;

        foreach (var type in assembly.GetTypes())
        {
            if (typeof(ICommand).IsAssignableFrom(type))
            {
                if (Activator.CreateInstance(type) is ICommand result)
                {
                    count++;
                    yield return result;
                }
            }
        }

        if (count == 0)
        {
            var availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
            throw new ApplicationException(
                $"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
                $"Available types: {availableTypes}");
        }
    }
}