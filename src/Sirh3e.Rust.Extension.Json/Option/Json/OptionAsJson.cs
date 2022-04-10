namespace Sirh3e.Rust.Extension.Json.Option;

public class OptionAsJson<TSome>
{
    [JsonPropertyName("some")]
    public TSome? Some { get; init; }

    [JsonPropertyName("isSome")]
    public bool IsSome { get; init; }

    public static implicit operator Option<TSome>(OptionAsJson<TSome> json) => json.IsSome
        ? Option<TSome>.Some(json.Some ?? throw new ArgumentNullException(nameof(json.Some)))
        : Option<TSome>.None;

    public static implicit operator OptionAsJson<TSome>(Option<TSome> option) => new()
    {
        Some = option.IsSome
            ? option.Unwrap()
            : default,
        IsSome = option.IsSome
    };
}