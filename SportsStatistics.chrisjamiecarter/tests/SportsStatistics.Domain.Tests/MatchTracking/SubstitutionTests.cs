using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Tests.MatchTracking.TestCases;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.MatchTracking;

public class SubstitutionTests
{
    [Theory]
    [ClassData(typeof(SubstitutionInvalidTestCase))]
    public void Create_ShouldReturnFailureResult_WhenSubstitutionIsInvalid(Guid? playerOffId, Guid? playerOnId, Error expected)
    {
        // Arrange.
        // Act.
        var result = Substitution.Create(playerOffId, playerOnId);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(SubstitutionValidTestCase))]
    public void Create_ShouldReturnSuccessResult_WhenSubstitutionIsValid(Guid playerOffId, Guid playerOnId, Substitution expected)
    {
        // Arrange.
        // Act.
        var result = Substitution.Create(playerOffId, playerOnId);

        // Assert.
        result.Value.ShouldBeEquivalentTo(expected);
    }
}
