namespace Sirh3e.Rust.Extension.Json.Test.Result;

public partial class ResultAsJsonUnitTest
{
    [Fact]
    public void ResultAsJson_Extension_Result_Ok_ToJson()
    {
        Result<string, int> result = Ok("Marvin");

        result.IsOk.Should().BeTrue();
        result.IsErr.Should().BeFalse();

        var json = result.ToJson();

        json.Should().NotBeNullOrEmpty();
        json.Should().NotBeNullOrWhiteSpace();

        json.Should().Be("{\"isOk\":true,\"ok\":\"Marvin\",\"err\":0}");
    }

    [Fact]
    public void ResultAsJson_Extension_Result_Err_ToJson()
    {
        Result<string, int> result = Err(42);

        result.IsOk.Should().BeFalse();
        result.IsErr.Should().BeTrue();

        var json = result.ToJson();

        json.Should().NotBeNullOrEmpty();
        json.Should().NotBeNullOrWhiteSpace();

        json.Should().Be("{\"isOk\":false,\"ok\":null,\"err\":42}");
    }

    [Fact]
    public void ResultAsJson_Extension_ResultAsJson_Ok_ToJson()
    {
        var @object = new ResultAsJson<string, int>
        {
            IsOk = true,
            Ok   = "Marvin"
        };

        @object.IsOk.Should().BeTrue();
        @object.Ok.Should().Be("Marvin");
        @object.Err.Should().Be(0);

        var json = @object.ToJson();

        json.Should().Be("{\"isOk\":true,\"ok\":\"Marvin\",\"err\":0}");
    }

    [Fact]
    public void ResultAsJson_Extension_ResultAsJson_Err_ToJson()
    {
        var @object = new ResultAsJson<string, int>
        {
            IsOk = false,
            Err  = 42
        };

        @object.IsOk.Should().BeFalse();
        @object.Ok.Should().Be(null);
        @object.Err.Should().Be(42);

        var json = @object.ToJson();

        json.Should().Be("{\"isOk\":false,\"ok\":null,\"err\":42}");
    }
}