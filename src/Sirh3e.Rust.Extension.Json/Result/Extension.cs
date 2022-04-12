namespace Sirh3e.Rust.Extension.Json.Result;

public static class Extension
{
    public static string ToJson<TOk, TErr>(this Result<TOk, TErr>       result) => ToJson((ResultAsJson<TOk, TErr>)result);
    public static string ToJson<TOk, TErr>(this ResultAsJson<TOk, TErr> json)   => JsonSerializer.Serialize(json);

    public static ResultAsJson<TOk, TErr> ToJsonObject<TOk, TErr>(this Result<TOk, TErr>       result)  => result;
    public static Result<TOk, TErr>       ToResult<TOk, TErr>(this     ResultAsJson<TOk, TErr> @object) => @object;

    public static Result<TOk, TErr> FromJsonToResult<TOk, TErr>(this string text) => JsonSerializer.Deserialize<ResultAsJson<TOk, TErr>>(text) ?? throw new ArgumentNullException(nameof(text));
}