# Solution Steps

1. Open `src/RecruitingTool/InterviewFeedbackAnalyzer.cs` and keep the existing null guard so `Summarize(null)` still throws `ArgumentNullException`.

2. Change the collected `records` list so it contains only completed feedback: filter the input with `Where(r => r.IsComplete)` before calling `ToList()`. This ensures incomplete feedback cannot affect counts, recommendations, rejections, or scores.

3. Build a separate list of usable numeric scores from the completed records by filtering to `Score.HasValue` and selecting the score value. Null scores remain counted as completed feedback, but are excluded from the average calculation.

4. Compute `AverageScore` safely: if the usable score list is empty, return `0d`; otherwise call `Average()` on the numeric score list. This avoids the exception that `Average()` would throw on an empty sequence.

5. Return the `FeedbackSummary` using the completed `records` list for `CountedFeedback`, `RecommendCount`, and `RejectCount`, and the safely computed score average for `AverageScore`.

6. Run the test project with `dotnet test` to verify empty input, incomplete feedback, missing scores, mixed input, and null input all behave correctly.

