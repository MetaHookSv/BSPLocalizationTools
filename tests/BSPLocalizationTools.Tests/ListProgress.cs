namespace BSPLocalizationTools.Tests;

// Synchronous IProgress so progress events are recorded before the awaited call returns.
// Progress<T> posts callbacks to the captured SynchronizationContext, which under xUnit can
// deliver them after the test's assertions have already run.
internal sealed class ListProgress(List<TranslationProgress> events) : IProgress<TranslationProgress>
{
    public void Report(TranslationProgress value)
    {
        events.Add(value);
    }
}
