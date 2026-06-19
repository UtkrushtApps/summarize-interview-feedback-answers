using RecruitingTool;
using Xunit;

namespace RecruitingTool.Tests;

public class InterviewFeedbackAnalyzerTests
{
    private readonly InterviewFeedbackAnalyzer _analyzer = new();

    [Fact]
    public void EmptyInput_ProducesZeroedSummary()
    {
        var result = _analyzer.Summarize(new List<InterviewFeedback>());

        Assert.Equal(0, result.CountedFeedback);
        Assert.Equal(0d, result.AverageScore);
        Assert.Equal(0, result.RecommendCount);
        Assert.Equal(0, result.RejectCount);
    }

    [Fact]
    public void OnlyIncompleteFeedback_IsIgnoredAndDoesNotCrash()
    {
        var feedback = new List<InterviewFeedback>
        {
            new() { CandidateId = "CAND-1", Score = 8, Recommendation = Recommendation.Recommend, IsComplete = false },
            new() { CandidateId = "CAND-1", Score = 5, Recommendation = Recommendation.Reject, IsComplete = false }
        };

        var result = _analyzer.Summarize(feedback);

        Assert.Equal(0, result.CountedFeedback);
        Assert.Equal(0d, result.AverageScore);
        Assert.Equal(0, result.RecommendCount);
        Assert.Equal(0, result.RejectCount);
    }

    [Fact]
    public void IncompleteFeedback_DoesNotContributeToCounts()
    {
        var feedback = new List<InterviewFeedback>
        {
            new() { CandidateId = "CAND-2", Score = 9, Recommendation = Recommendation.Recommend, IsComplete = true },
            new() { CandidateId = "CAND-2", Score = 4, Recommendation = Recommendation.Reject, IsComplete = false },
            new() { CandidateId = "CAND-2", Score = 7, Recommendation = Recommendation.Recommend, IsComplete = true }
        };

        var result = _analyzer.Summarize(feedback);

        Assert.Equal(2, result.CountedFeedback);
        Assert.Equal(8d, result.AverageScore);
        Assert.Equal(2, result.RecommendCount);
        Assert.Equal(0, result.RejectCount);
    }

    [Fact]
    public void MissingScores_AreExcludedFromAverage()
    {
        var feedback = new List<InterviewFeedback>
        {
            new() { CandidateId = "CAND-3", Score = 10, Recommendation = Recommendation.Recommend, IsComplete = true },
            new() { CandidateId = "CAND-3", Score = null, Recommendation = Recommendation.Recommend, IsComplete = true },
            new() { CandidateId = "CAND-3", Score = 6, Recommendation = Recommendation.Reject, IsComplete = true }
        };

        var result = _analyzer.Summarize(feedback);

        Assert.Equal(3, result.CountedFeedback);
        Assert.Equal(8d, result.AverageScore);
        Assert.Equal(2, result.RecommendCount);
        Assert.Equal(1, result.RejectCount);
    }

    [Fact]
    public void CompletedFeedbackWithNoScores_ProducesZeroAverageWithoutCrashing()
    {
        var feedback = new List<InterviewFeedback>
        {
            new() { CandidateId = "CAND-4", Score = null, Recommendation = Recommendation.Reject, IsComplete = true },
            new() { CandidateId = "CAND-4", Score = null, Recommendation = Recommendation.Recommend, IsComplete = true }
        };

        var result = _analyzer.Summarize(feedback);

        Assert.Equal(2, result.CountedFeedback);
        Assert.Equal(0d, result.AverageScore);
        Assert.Equal(1, result.RecommendCount);
        Assert.Equal(1, result.RejectCount);
    }

    [Fact]
    public void MixedFeedback_ProducesCorrectSummary()
    {
        var feedback = new List<InterviewFeedback>
        {
            new() { CandidateId = "CAND-5", Score = 8, Recommendation = Recommendation.Recommend, IsComplete = true },
            new() { CandidateId = "CAND-5", Score = 6, Recommendation = Recommendation.Recommend, IsComplete = true },
            new() { CandidateId = "CAND-5", Score = 2, Recommendation = Recommendation.Reject, IsComplete = true },
            new() { CandidateId = "CAND-5", Score = 9, Recommendation = Recommendation.Recommend, IsComplete = false },
            new() { CandidateId = "CAND-5", Score = null, Recommendation = Recommendation.Reject, IsComplete = true }
        };

        var result = _analyzer.Summarize(feedback);

        Assert.Equal(4, result.CountedFeedback);
        Assert.Equal(16d / 3d, result.AverageScore, 5);
        Assert.Equal(2, result.RecommendCount);
        Assert.Equal(2, result.RejectCount);
    }

    [Fact]
    public void NullInput_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => _analyzer.Summarize(null!));
    }
}
