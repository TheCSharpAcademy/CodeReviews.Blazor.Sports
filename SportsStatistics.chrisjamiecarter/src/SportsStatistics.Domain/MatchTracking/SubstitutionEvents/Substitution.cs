using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.SubstitutionEvents;

public sealed record Substitution
{
    private Substitution(Guid playerOffId, Guid playerOnId)
    {
        PlayerOffId = playerOffId;
        PlayerOnId = playerOnId;
    }

    public Guid PlayerOffId { get; private set; }

    public Guid PlayerOnId { get; private set; }

    public static Result<Substitution> Create(Guid? playerOffId, Guid? playerOnId)
    {
        if (playerOffId is null || playerOffId == Guid.Empty)
        {
            return SubstitutionEventErrors.Substitution.PlayerOffIdNullOrEmpty;
        }

        if (playerOnId is null || playerOnId == Guid.Empty)
        {
            return SubstitutionEventErrors.Substitution.PlayerOnIdNullOrEmpty;
        }

        if (playerOffId == playerOnId)
        {
            return SubstitutionEventErrors.Substitution.SamePlayer;
        }

        return new Substitution(playerOffId.Value, playerOnId.Value);
    }
}
