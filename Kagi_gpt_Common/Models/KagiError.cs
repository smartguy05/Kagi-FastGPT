namespace Kagi_gpt_Common.Models;

public record KagiError
{
    public int Code { get; set; }
    public string Msg { get; set; }
    public string Ref { get; set; }
}