namespace Sirh3e.Rust.Extension.Json.Test.Option.Json;

public partial class OptionAsJsonUnitTest
{
    [Fact]
    public void OptionAsJson_IsSome_True()
    {
        var json = new OptionAsJson<string>
        {
            Some   = "Marvin",
            IsSome = true
        };

        json.Some.Should().Be("Marvin");
        json.IsSome.Should().BeTrue();
    }

    [Fact]
    public void OptionAsJson_IsSome_False()
    {
        var json = new OptionAsJson<string>
        {
            Some   = null,
            IsSome = false
        };

        json.Some.Should().BeNull();
        json.IsSome.Should().BeFalse();
    }

    [Fact]
    public void OptionAsJson_Implicit_Operator_Option_TSome_Is_True()
    {
        var json = new OptionAsJson<string>
        {
            Some   = "Marvin",
            IsSome = true
        };

        Option<string> option = json;

        option.IsSome.Should().BeTrue();
        option.IsNone.Should().BeFalse();

        var provider = () => option.Unwrap();
        provider.Should().NotThrow();
        provider().Should().Be("Marvin");
    }

    [Fact]
    public void OptionAsJson_Implicit_Operator_Option_TSome_Is_False()
    {
        var json = new OptionAsJson<string>
        {
            Some   = null,
            IsSome = false
        };

        Option<string> option = json;

        option.IsSome.Should().BeFalse();
        option.IsNone.Should().BeTrue();

        var provider = () => option.Unwrap();
        provider.Should().ThrowExactly<PanicException>();
    }

    [Fact]
    public void Option_Implicit_Operator_OptionAsJson_Is_True()
    {
        var some = Some("Marvin");

        some.IsSome.Should().BeTrue();
        some.IsNone.Should().BeFalse();

        var provider = () => some.Unwrap();
        provider.Should().NotThrow();
        provider().Should().Be("Marvin");

        OptionAsJson<string> json = some;

        json.IsSome.Should().BeTrue();
        json.Some.Should().Be("Marvin");
    }

    [Fact]
    public void Option_Implicit_Operator_OptionAsJson_Is_False()
    {
        var none = Option<string>.None;

        none.IsSome.Should().BeFalse();
        none.IsNone.Should().BeTrue();

        OptionAsJson<string> json = none;

        json.IsSome.Should().BeFalse();
        json.Some.Should().BeNull();

        Option<string> option = json;

        option.IsNone.Should().BeTrue();
        option.IsSome.Should().BeFalse();

        var provider = () => option.Unwrap();
        provider.Should().ThrowExactly<PanicException>();
    }
}