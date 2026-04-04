using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Tests.MatchTracking.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.MatchTracking.TestCases;

public class SubstitutionInvalidTestCase : TheoryData<Guid?, Guid?, Error>
{
    public SubstitutionInvalidTestCase()
    {
        Add(SubstitutionTestData.NullPlayerId, SubstitutionTestData.ValidPlayerOnId, SubstitutionEventErrors.Substitution.PlayerOffIdNullOrEmpty);
        Add(SubstitutionTestData.EmptyPlayerId, SubstitutionTestData.ValidPlayerOnId, SubstitutionEventErrors.Substitution.PlayerOffIdNullOrEmpty);
        Add(SubstitutionTestData.ValidPlayerOffId, SubstitutionTestData.NullPlayerId, SubstitutionEventErrors.Substitution.PlayerOnIdNullOrEmpty);
        Add(SubstitutionTestData.ValidPlayerOffId, SubstitutionTestData.EmptyPlayerId, SubstitutionEventErrors.Substitution.PlayerOnIdNullOrEmpty);
        Add(SubstitutionTestData.ValidPlayerOffId, SubstitutionTestData.ValidPlayerOffId, SubstitutionEventErrors.Substitution.SamePlayer);
    }
}
