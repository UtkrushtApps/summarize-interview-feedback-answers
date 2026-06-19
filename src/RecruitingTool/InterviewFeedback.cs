namespace RecruitingTool;

/// <summary>
/// A single interviewer's feedback for a candidate after an interview round.
/// </summary>
public record InterviewFeedback
{
    /// <summary>Identifier of the candidate this feedback belongs to.</summary>
    public required string CandidateId { get; init; }

    /// <summary>The numeric interview score, if one was recorded.</summary>
    public int? Score { get; init; }

    /// <summary>The recommendation the interviewer made.</summary>
    public Recommendation Recommendation { get; init; }

    /// <summary>Whether the interviewer finished submitting this feedback.</summary>
    public bool IsComplete { get; init; }
}
