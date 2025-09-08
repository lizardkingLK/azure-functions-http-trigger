using System.Text.Json.Serialization;

namespace AzureFunctionSample.Configurations.Options;

public class TagOptions
{
    public required string Name { get; init; }
    [JsonPropertyName("value")] public required string Value { get; init; }
}

public class SampleOptions
{
    public required string Color { get; init; }
    public required bool IsActive { get; init; }
    public required int Place { get; init; }
    [JsonPropertyName("tags")] public required TagOptions[] Tags { get; init; }
    public required string BaseAPIAddress { get; init; }
}