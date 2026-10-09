using System.Text.Json.Serialization;

namespace BSPLocalizationTools;

// Concrete request shapes for the OpenAI-compatible endpoints. They replace the
// former anonymous types so Native AOT and trimming keep the serialization
// metadata (the anonymous-type JsonSerializer overloads are not AOT-compatible).
// Default serializer options are used, so escaping matches the previous output.
internal sealed record ChatCompletionRequest(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("messages")] ChatRequestMessage[] Messages,
    [property: JsonPropertyName("reasoning_effort")] string ReasoningEffort,
    [property: JsonPropertyName("temperature")] [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] double? Temperature);

internal sealed record ChatRequestMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content);

internal sealed record ResponsesRequest(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("input")] ResponsesInputMessage[] Input,
    [property: JsonPropertyName("reasoning")] ResponsesReasoning Reasoning,
    [property: JsonPropertyName("stream")] bool Stream,
    [property: JsonPropertyName("temperature")] [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] double? Temperature);

internal sealed record ResponsesInputMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content);

internal sealed record ResponsesReasoning(
    [property: JsonPropertyName("effort")] string Effort);

internal sealed record PromptPayload(
    [property: JsonPropertyName("target_language")] string TargetLanguage,
    [property: JsonPropertyName("output_contract")] PromptOutputContract OutputContract,
    [property: JsonPropertyName("inputs")] PromptInput[] Inputs);

internal sealed record PromptOutputContract(
    [property: JsonPropertyName("format")] string Format,
    [property: JsonPropertyName("schema")] string Schema,
    [property: JsonPropertyName("rules")] string[] Rules);

internal sealed record PromptInput(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("text")] string Text);

[JsonSerializable(typeof(ChatCompletionRequest))]
[JsonSerializable(typeof(ResponsesRequest))]
[JsonSerializable(typeof(PromptPayload))]
internal sealed partial class LlmRequestJsonContext : JsonSerializerContext
{
}
