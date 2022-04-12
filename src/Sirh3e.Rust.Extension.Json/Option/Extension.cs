namespace Sirh3e.Rust.Extension.Json.Option;

public static class Extension
{
    public static string ToJson<TSome>(this Option<TSome>       option) => ToJson((OptionAsJson<TSome>)option);
    public static string ToJson<TSome>(this OptionAsJson<TSome> json)   => JsonSerializer.Serialize(json);

    public static OptionAsJson<TSome> ToJsonObject<TSome>(this Option<TSome>       option)  => option;
    public static Option<TSome>       ToOption<TSome>(this     OptionAsJson<TSome> @object) => @object;

    public static Option<TSome> FromJsonToOption<TSome>(this string text) => JsonSerializer.Deserialize<OptionAsJson<TSome>>(text) ?? Option<TSome>.None;
}