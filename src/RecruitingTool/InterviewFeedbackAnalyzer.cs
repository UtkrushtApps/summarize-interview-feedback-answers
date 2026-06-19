namespace RecruitingTool;

/// <summary>
/// Builds a <see cref="FeedbackSummary"/> from a collection of interview feedback records.
/// </summary>
public class InterviewFeedbackAnalyzer
{
    /// <summary>
    /// Produces an aggregated summary of the supplied interview feedback.
    /// </summary>
    public FeedbackSummary Summarize(IEnumerable<InterviewFeedback> feedback)
    {
        if (feedback is null)
        {
            throw new ArgumentNullException(nameof(feedback));
        }

        var records = feedback
            .Where(r => r.IsComplete)
            .ToList();

        var scores = records
            .Where(r => r.Score.HasValue)
            .Select(r => r.Score!.Value)
            .ToList();

        var average = scores.Count == 0 ? 0d : scores.Average();

        return new FeedbackSummary
        {
            CountedFeedback = records.Count,
            AverageScore = average,
            RecommendCount = records.Count(r => r.Recommendation == Recommendation.Recommend),
            RejectCount = records.Count(r => r.Recommendation == Recommendation.Reject)
        };
    }
}
