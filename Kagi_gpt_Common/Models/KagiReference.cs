namespace Kagi_gpt_Common.Models;

public record KagiReference
{
    public string Title { get; init; }
    public string Snippet { get; init; }
    public string Url { get; init; }
}