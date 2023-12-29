namespace Kagi_gpt_Common.Models;

public record KagiResponse
{
    public KagiMeta Meta { get; init; }
    public KagiData Data { get; init; }
    public KagiError[] Error { get; init; }
}