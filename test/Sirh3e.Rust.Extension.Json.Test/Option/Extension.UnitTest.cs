namespace Sirh3e.Rust.Extension.Json.Test.Option.Json;

public partial class OptionAsJsonUnitTest
{
    [Fact]
    public void OptionAsJson_Extension_Option_Some_ToJson()
    {
        var some = Some("Marvin");

        some.IsSome.Should().BeTrue();
        some.IsNone.Should().BeFalse();

        var json = some.ToJson();

        json.Should().NotBeNullOrEmpty();
        json.Should().NotBeNullOrWhiteSpace();

        json.Should().Be("{\"some\":\"Marvin\",\"isSome\":true}");
    }

    [Fact]
    public void OptionAsJson_Extension_Option_None_ToJson()
    {
        var none = Option<string>.None;

        none.IsSome.Should().BeFalse();
        none.IsNone.Should().BeTrue();

        var json = none.ToJson();

        json.Should().NotBeNullOrEmpty();
        json.Should().NotBeNullOrWhiteSpace();

        json.Should().Be("{\"some\":null,\"isSome\":false}");
    }

    [Fact]
    public void OptionAsJson_Extension_OptionAsJson_Some_ToJson()
    {
        OptionAsJson<string> some = Some("Marvin");

        some.IsSome.Should().BeTrue();
        some.Some.Should().Be("Marvin");

        var json = some.ToJson();

        json.Should().NotBeNullOrEmpty();
        json.Should().NotBeNullOrWhiteSpace();

        json.Should().Be("{\"some\":\"Marvin\",\"isSome\":true}");
    }

    [Fact]
    public void OptionAsJson_Extension_OptionAsJson_None_ToJson()
    {
        OptionAsJson<string> none = Option<string>.None;

        none.IsSome.Should().BeFalse();
        none.Some.Should().BeNull();

        var json = none.ToJson();

        json.Should().NotBeNullOrEmpty();
        json.Should().NotBeNullOrWhiteSpace();

        json.Should().Be("{\"some\":null,\"isSome\":false}");
    }

    [Fact]
    public void OptionAsJson_Extension_Option_Some_ToJsonObject()
    {
        var some = Some("Marvin");

        var json = some.ToJsonObject();

        json.Some.Should().Be("Marvin");
        json.IsSome.Should().BeTrue();
    }

    [Fact]
    public void OptionAsJson_Extension_Option_None_ToJsonObject()
    {
        var none = Option<string>.None;

        var json = none.ToJsonObject();

        json.Some.Should().BeNull();
        json.IsSome.Should().BeFalse();
    }

    [Theory]
    [InlineData("Mario")]
    [InlineData("Michi")]
    [InlineData("Issi")]
    [InlineData("Emanuel")]
    [InlineData("Marvin")]
    [InlineData("Stefan")]
    [InlineData("Corina")]
    public void OptionAsJson_Extension_OptionAsJson_Some_ToOption(string name)
    {
        var json = new OptionAsJson<string>
        {
            Some   = name,
            IsSome = true
        };

        var some = json.ToOption();

        some.IsSome.Should().BeTrue();
        some.IsNone.Should().BeFalse();

        var provider = () => some.Unwrap();

        provider.Should().NotThrow();
        provider().Should().Be(name);
    }

    [Fact]
    public void OptionAsJson_Extension_OptionAsJson_None_ToOption()
    {
        var json = new OptionAsJson<string>
        {
            Some   = null,
            IsSome = false
        };

        var some = json.ToOption();

        some.IsSome.Should().BeFalse();
        some.IsNone.Should().BeTrue();

        var provider = () => some.Unwrap();

        provider.Should().ThrowExactly<PanicException>();
    }

    [Fact]
    public void OptionAsJson_Extension_OptionAsJson_Some_FromJsonToOption()
    {
        var json = "{\"some\":\"Marvin\",\"isSome\":true}";

        var some = json.FromJsonToOption<string>();

        some.IsSome.Should().BeTrue();
        some.IsNone.Should().BeFalse();

        var provider = () => some.Unwrap();

        provider.Should().NotThrow();
        provider().Should().Be("Marvin");
    }

    [Fact]
    public void OptionAsJson_Extension_OptionAsJson_None_FromJsonToOption()
    {
        var json = "{\"some\":null,\"isSome\":false}";

        var some = json.FromJsonToOption<string>();

        some.IsSome.Should().BeFalse();
        some.IsNone.Should().BeTrue();

        var provider = () => some.Unwrap();

        provider.Should().ThrowExactly<PanicException>();
    }
}