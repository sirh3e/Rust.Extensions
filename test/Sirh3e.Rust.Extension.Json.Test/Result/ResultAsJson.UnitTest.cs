namespace Sirh3e.Rust.Extension.Json.Test.Result;

public partial class ResultAsJsonUnitTest
{
    [Fact]
    public void ResultAsJson_IsOk_True()
    {
        var json = new ResultAsJson<string, int>
        {
            Ok   = "Marvin",
            IsOk = true
        };

        json.IsOk.Should().BeTrue();
        json.Ok.Should().Be("Marvin");
        json.Err.Should().Be(0);
    }

    [Fact]
    public void ResultAsJson_IsOk_False()
    {
        var json = new ResultAsJson<string, int>
        {
            IsOk = false,
            Err  = 1
        };

        json.IsOk.Should().BeFalse();
        json.Ok.Should().Be(null);
        json.Err.Should().Be(1);
    }

    [Fact]
    public void ResultAsJson_Implicit_Operator_Result_IsOk_Is_True()
    {
        Result<string, int> result = Ok("Marvin");

        result.IsOk.Should().BeTrue();
        result.IsErr.Should().BeFalse();

        ResultAsJson<string, int> json = result;

        json.IsOk.Should().BeTrue();
        json.Ok.Should().Be("Marvin");
        json.Err.Should().Be(0);
    }

    [Fact]
    public void ResultAsJson_Implicit_Operator_Result_IsOk_Is_False()
    {
        Result<string, int> result = Err(42);

        result.IsOk.Should().BeFalse();
        result.IsErr.Should().BeTrue();

        ResultAsJson<string, int> json = result;

        json.IsOk.Should().BeFalse();
        json.Ok.Should().Be(null);
        json.Err.Should().Be(42);
    }

    [Fact]
    public void Result_Implicit_Operator_ResultAsJson_IsOk_Is_True()
    {
        var json = new ResultAsJson<string, int>
        {
            Ok   = "Marvin",
            IsOk = true
        };

        json.IsOk.Should().BeTrue();
        json.Ok.Should().Be("Marvin");
        json.Err.Should().Be(0);

        Result<string, int> result = json;

        result.IsOk.Should().BeTrue();
        result.IsErr.Should().BeFalse();

        var provider = () => result.Unwrap();
        provider.Should().NotThrow();
        provider().Should().Be("Marvin");
    }

    [Fact]
    public void Result_Implicit_Operator_ResultAsJson_IsOk_Is_False()
    {
        var json = new ResultAsJson<string, int>
        {
            IsOk = false,
            Err  = 1
        };

        json.IsOk.Should().BeFalse();
        json.Ok.Should().Be(null);
        json.Err.Should().Be(1);

        Result<string, int> result = json;

        result.IsOk.Should().BeFalse();
        result.IsErr.Should().BeTrue();

        var provider = () => result.Unwrap();
        provider.Should().ThrowExactly<PanicException>();
    }

    [Fact]
    public void ResultAsJson_Result_IsOK_True_ToJsonObject()
    {
        Result<string, int> result = Ok("Marvin");

        result.IsOk.Should().BeTrue();
        result.IsErr.Should().BeFalse();

        var @object = result.ToJsonObject();

        @object.IsOk.Should().BeTrue();
        @object.Ok.Should().Be("Marvin");
        @object.Err.Should().Be(0);
    }

    [Fact]
    public void ResultAsJson_Result_IsOK_False_ToJsonObject()
    {
        Result<string, int> result = Err(42);

        result.IsOk.Should().BeFalse();
        result.IsErr.Should().BeTrue();

        var @object = result.ToJsonObject();

        @object.IsOk.Should().BeFalse();
        @object.Ok.Should().Be(null);
        @object.Err.Should().Be(42);
    }

    [Fact]
    public void Result_ResultAsJson_IsOK_True_ToJsonObject()
    {
        var json = new ResultAsJson<string, int>
        {
            Ok   = "Marvin",
            IsOk = true
        };

        json.IsOk.Should().BeTrue();
        json.Ok.Should().Be("Marvin");
        json.Err.Should().Be(0);

        var result = json.ToResult();

        result.IsOk.Should().BeTrue();
        result.IsErr.Should().BeFalse();

        var provider = () => result.Unwrap();
        provider.Should().NotThrow();
        provider().Should().Be("Marvin");
    }

    [Fact]
    public void Result_ResultAsJson_IsOK_False_ToJsonObject()
    {
        var json = new ResultAsJson<string, int>
        {
            IsOk = false,
            Err  = 1
        };

        json.IsOk.Should().BeFalse();
        json.Ok.Should().Be(null);
        json.Err.Should().Be(1);

        var result = json.ToResult();

        result.IsOk.Should().BeFalse();
        result.IsErr.Should().BeTrue();

        var provider = () => result.Unwrap();
        provider.Should().ThrowExactly<PanicException>();
    }

    [Fact]
    public void JsonString_Result_IsOK_True_FromJsonToResult()
    {
        var text = "{\"isOk\":true,\"ok\":\"Marvin\",\"err\":0}";

        var result = text.FromJsonToResult<string, int>();

        result.IsOk.Should().BeTrue();
        result.IsErr.Should().BeFalse();

        var okProvider = () => result.Unwrap();
        okProvider.Should().NotThrow();
        okProvider().Should().Be("Marvin");

        var errProvider = () => result.UnwrapErr();
        errProvider.Should().ThrowExactly<PanicException>();
    }

    [Fact]
    public void JsonString_Result_IsOK_False_FromJsonToResult()
    {
        var text = "{\"isOk\":false,\"ok\":null,\"err\":42}";

        var result = text.FromJsonToResult<string, int>();

        result.IsOk.Should().BeFalse();
        result.IsErr.Should().BeTrue();

        var okProvider = () => result.Unwrap();
        okProvider.Should().ThrowExactly<PanicException>();

        var errProvider = () => result.UnwrapErr();
        errProvider.Should().NotThrow();
        errProvider().Should().Be(42);
    }
}