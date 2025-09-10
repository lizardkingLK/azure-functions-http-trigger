using System.ComponentModel.DataAnnotations;
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

    [Required(ErrorMessage = "error. env var BaseAPIAddress is not set")]
    public required string BaseAPIAddress { get; init; }
}