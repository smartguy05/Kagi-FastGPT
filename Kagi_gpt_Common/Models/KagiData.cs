namespace Kagi_gpt_Common.Models;

public record KagiData
{
    public string Output { get; init; }
    public int Tokens { get; init; }
    public KagiReference[] References { get; init; }
}