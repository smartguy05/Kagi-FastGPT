using Kagi_gpt_Common.Interfaces;

namespace Test_Plugin;

public class TestCommand : ICommand
{
    public string Name => "TEST";
    public string Description  => "Displays hello message.";

    public int Execute(params string[] args)
    {
        Console.WriteLine("TEST SUCCESSFUL!!!");
        if (args.Length > 0)
        {
            for (var i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"Param {i+1}");
                Console.WriteLine(args[i]);
            }
        }
        else
        {
            Console.WriteLine("No args supplied.");
        }
        
        return 0;
    }
}