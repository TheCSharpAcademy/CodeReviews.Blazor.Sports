using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Tests.MatchTracking.TestData;

namespace SportsStatistics.Domain.Tests.MatchTracking.TestCases;

public class SubstitutionValidTestCase : TheoryData<Guid, Guid, Substitution>
{
    private static readonly Substitution Substitution = SubstitutionTestData.ValidSubstitution;

    public SubstitutionValidTestCase()
    {
        Add(Substitution.PlayerOffId, Substitution.PlayerOnId, Substitution);
    }
}
