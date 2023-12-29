namespace Kagi_gpt_Common.Interfaces;

public interface ICommand
{
    public string Name { get; }
    public string Description { get; }

    int Execute(params string[] args);
}