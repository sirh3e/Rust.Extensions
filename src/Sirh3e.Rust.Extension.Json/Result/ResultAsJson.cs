namespace Sirh3e.Rust.Extension.Json.Result;

public class ResultAsJson<TOk, TErr>
{
    [JsonPropertyName("isOk")]
    public bool IsOk { get; init; }

    [JsonPropertyName("ok")]
    public TOk? Ok { get; init; }

    [JsonPropertyName("err")]
    public TErr? Err { get; init; }

    public static implicit operator Result<TOk, TErr>(ResultAsJson<TOk, TErr> json) => json.IsOk
        ? new Ok<TOk>(json.Ok    ?? throw new ArgumentNullException(nameof(json.Ok)))
        : new Err<TErr>(json.Err ?? throw new ArgumentNullException(nameof(json.Err)));

    public static implicit operator ResultAsJson<TOk, TErr>(Result<TOk, TErr> result) => new()
    {
        IsOk = result.IsOk,
        Ok = result.IsOk
            ? result.Unwrap()
            : default,
        Err = result.IsErr
            ? result.UnwrapErr()
            : default
    };
}