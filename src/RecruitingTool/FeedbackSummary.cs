namespace RecruitingTool;

/// <summary>
/// Aggregated view of interview feedback used by hiring managers.
/// </summary>
public record FeedbackSummary
{
    /// <summary>Number of feedback records that were counted in the summary.</summary>
    public int CountedFeedback { get; init; }

    /// <summary>Average of the recorded scores among the counted feedback.</summary>
    public double AverageScore { get; init; }

    /// <summary>Number of counted feedback records recommending the candidate.</summary>
    public int RecommendCount { get; init; }

    /// <summary>Number of counted feedback records rejecting the candidate.</summary>
    public int RejectCount { get; init; }
}
