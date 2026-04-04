using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestCases;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players;

public class NameTests
{
    [Theory]
    [ClassData(typeof(NameInvalidTestCase))]
    public void Create_ShouldReturnFailureResult_WhenNameIsInvalid(string? name, Error expected)
    {
        // Arrange.
        // Act.
        var result = Name.Create(name);

        // Assert.
        result.Error.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(NameValidTestCase))]
    public void Create_ShouldReturnSuccessResult_WhenNameIsValid(string name, Name expected)
    {
        // Arrange.
        // Act.
        var result = Name.Create(name);

        // Assert.
        result.Value.ShouldBeEquivalentTo(expected);
    }
}
