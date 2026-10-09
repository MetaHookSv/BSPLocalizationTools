using System.Text.Json;

namespace BSPLocalizationTools;

public static class TranslationPromptBuilder
{
    public const string BuiltInPrompt = """
        Translate GoldSrc game_text messages into the requested target language.
        Preserve literal \n sequences, punctuation intensity, urgency, and proper nouns when appropriate.
        Return natural in-game text, not explanatory notes.
        Keep what it was if the given game_text message is non-translatable. i.e. when given game_text message is numeric only.
        """;

    public static IReadOnlyList<LLMMessage> Build(
        string targetLanguage,
        string? customPrompt,
        IReadOnlyList<string> sourceMessages)
    {
        var instructions = string.IsNullOrWhiteSpace(customPrompt)
            ? BuiltInPrompt
            : customPrompt.Trim();
        var payload = new PromptPayload(
            targetLanguage,
            new PromptOutputContract(
                "json",
                """{"translations":[{"id":0,"translation":"..."}]}""",
                [
                    "Return only valid JSON.",
                    "Include every input id exactly once.",
                    "Keep literal \\n as backslash-n text.",
                ]),
            sourceMessages.Select((text, id) => new PromptInput(id, text)).ToArray());

        return
        [
            new LLMMessage("system", instructions),
            new LLMMessage("user", JsonSerializer.Serialize(payload, LlmRequestJsonContext.Default.PromptPayload)),
        ];
    }
}
