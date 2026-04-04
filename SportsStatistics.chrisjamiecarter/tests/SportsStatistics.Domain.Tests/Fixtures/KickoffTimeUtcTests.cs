using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestCases;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Fixtures;

public class KickoffTimeUtcTests
{
    [Theory]
    [ClassData(typeof(KickoffTimeUtcInvalidTestCase))]
    public void Create_ShouldReturnFailureResult_WhenKickoffTimeUtcIsInvalid(DateTime? kickoffTimeUtc, Error expected)
    {
        // Arrange.
        // Act.
        var result = KickoffTimeUtc.Create(kickoffTimeUtc);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(KickoffTimeUtcValidTestCase))]
    public void Create_ShouldReturnSuccessResult_WhenKickoffTimeUtcIsValid(DateTime kickoffTimeUtc, KickoffTimeUtc expected)
    {
        // Arrange.
        // Act.
        var result = KickoffTimeUtc.Create(kickoffTimeUtc);

        // Assert.
        result.Value.ShouldBeEquivalentTo(expected);
    }
}
