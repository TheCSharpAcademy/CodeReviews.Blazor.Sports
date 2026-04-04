using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.MatchTracking.TestData;

public class SubstitutionTestData
{
    public static readonly Guid ValidPlayerOffId = PlayerTestData.ValidPlayer.Id;

    public static readonly Guid ValidPlayerOnId = PlayerTestData.ValidPlayer.Id;

    public static readonly Guid? NullPlayerId;

    public static readonly Guid EmptyPlayerId = Guid.Empty;

    public static Substitution ValidSubstitution => Substitution.Create(ValidPlayerOffId, ValidPlayerOnId).Value;
}
