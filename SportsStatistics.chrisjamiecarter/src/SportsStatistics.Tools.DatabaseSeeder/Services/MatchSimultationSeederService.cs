using Bogus;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;
using SportsStatistics.Tools.DatabaseSeeder.TestData;

namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface IMatchSimultationSeederService : ISeederService { }

internal sealed class MatchSimultationSeederService(
    ApplicationDbContext dbContext,
    ILogger<MatchSimultationSeederService> logger)
    : IMatchSimultationSeederService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<MatchSimultationSeederService> _logger = logger;

    public async Task<Result> SeedAsync(CancellationToken cancellationToken = default)
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Starting seeding for match simulations.");

            var faker = new Faker();

            var fixtures = await _dbContext.Fixtures
                .Where(fixture => fixture.KickoffTimeUtc.Value < DateTime.UtcNow)
                .ToListAsync(cancellationToken);

            if (fixtures.Count == 0)
            {
                _logger.LogInformation("No fixtures to simulate.");
                return Result.Success();
            }

            var players = await _dbContext.Players
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            foreach (var fixture in fixtures)
            {
                fixture.ChangeStatus(Status.InProgress);

                var matchEvents = new List<MatchEvent>();
                var playerEvents = new List<PlayerEvent>();
                var substitutionEvents = new List<SubstitutionEvent>();

                // ---------------------------------------------------------------------------------
                // Setup.
                // ---------------------------------------------------------------------------------

                var firstHalfMinutes = faker.Random.Int(45, 50);
                var secondHalfMinutes = faker.Random.Int(45, 50);
                var substutitionMinute = faker.Random.Int(60, 70);

                var homeGoals = faker.Random.Int(0, 5);
                var awayGoals = faker.Random.Int(0, 5);
                var yellowCardCount = faker.Random.Int(0, 5);
                var redCard = faker.Random.Int(1, 10) == 10;

                // ---------------------------------------------------------------------------------
                // Pre Match.
                // ---------------------------------------------------------------------------------

                var currentPeriod = MatchPeriod.PreMatch;

                var teamsheet = new TeamsheetFaker(
                    fixture,
                    players)
                .Generate(1)
                .First();

                var starterIds = teamsheet.GetStarterIds();
                var substituteIds = teamsheet.GetSubstituteIds();

                var playersOnField = players
                    .Where(player => starterIds.Contains(player.Id))
                    .ToList();

                var playersOnBench = players
                    .Where(player => substituteIds.Contains(player.Id))
                    .ToList();

                // ---------------------------------------------------------------------------------
                // First Half.
                // ---------------------------------------------------------------------------------

                var occurredAtUtc = fixture.KickoffTimeUtc.Value;
                var firstHalfStartedAtUtc = occurredAtUtc;
                var firstHalfEndAtUtc = occurredAtUtc.AddMinutes(firstHalfMinutes);

                matchEvents.Add(MatchEvent.Create(
                    fixture.Id,
                    MatchEventType.FirstHalfStarted,
                    MatchMinuteCalculator.Calculate(occurredAtUtc, firstHalfStartedAtUtc, null, currentPeriod),
                    occurredAtUtc));

                currentPeriod = MatchPeriod.FirstHalf;

                SimulateHalf(
                    faker,
                    fixture.Id,
                    playersOnField,
                    firstHalfStartedAtUtc,
                    firstHalfEndAtUtc,
                    firstHalfStartedAtUtc,
                    null,
                    currentPeriod,
                    playerEvents);

                // ---------------------------------------------------------------------------------
                // Half Time.
                // ---------------------------------------------------------------------------------

                occurredAtUtc = firstHalfEndAtUtc;

                matchEvents.Add(MatchEvent.Create(
                    fixture.Id,
                    MatchEventType.FirstHalfFinished,
                    MatchMinuteCalculator.Calculate(occurredAtUtc, firstHalfStartedAtUtc, null, currentPeriod),
                    occurredAtUtc));

                currentPeriod = MatchPeriod.HalfTime;

                // Half time is 15 minutes.
                occurredAtUtc = occurredAtUtc.AddMinutes(15);

                // ---------------------------------------------------------------------------------
                // Second Half.
                // ---------------------------------------------------------------------------------

                var secondHalfStartedAtUtc = occurredAtUtc;
                var substutitionAtUtc = occurredAtUtc.AddMinutes(substutitionMinute - 45);
                var secondHalfEndAtUtc = occurredAtUtc.AddMinutes(secondHalfMinutes);

                matchEvents.Add(MatchEvent.Create(
                    fixture.Id,
                    MatchEventType.SecondHalfStarted,
                    MatchMinuteCalculator.Calculate(occurredAtUtc, firstHalfStartedAtUtc, secondHalfStartedAtUtc, currentPeriod),
                    occurredAtUtc));

                currentPeriod = MatchPeriod.SecondHalf;

                SimulateHalf(
                    faker,
                    fixture.Id,
                    playersOnField,
                    secondHalfStartedAtUtc,
                    substutitionAtUtc,
                    firstHalfStartedAtUtc,
                    secondHalfStartedAtUtc,
                    currentPeriod,
                    playerEvents);

                substitutionEvents.Add(new SubstitutionEventFaker(
                    fixture.Id,
                    substutitionAtUtc,
                    firstHalfStartedAtUtc,
                    secondHalfStartedAtUtc,
                    currentPeriod,
                    playersOnField,
                    playersOnBench)
                    .Generate(1)
                    .First());

                SimulateHalf(
                    faker,
                    fixture.Id,
                    playersOnField,
                    substutitionAtUtc,
                    secondHalfEndAtUtc,
                    firstHalfStartedAtUtc,
                    secondHalfStartedAtUtc,
                    currentPeriod,
                    playerEvents);

                occurredAtUtc = secondHalfEndAtUtc;

                matchEvents.Add(MatchEvent.Create(
                    fixture.Id,
                    MatchEventType.SecondHalfFinished,
                    MatchMinuteCalculator.Calculate(occurredAtUtc, firstHalfStartedAtUtc, secondHalfStartedAtUtc, currentPeriod),
                    occurredAtUtc));

                currentPeriod = MatchPeriod.FullTime;

                // ---------------------------------------------------------------------------------
                // Goals and cards (inserted at random minutes after simulation).
                // ---------------------------------------------------------------------------------

                AddGoalEvents(
                    faker,
                    fixture,
                    homeGoals,
                    awayGoals,
                    playersOnField,
                    firstHalfStartedAtUtc,
                    firstHalfEndAtUtc,
                    secondHalfStartedAtUtc,
                    secondHalfEndAtUtc,
                    playerEvents);

                AddCardEvents(
                    faker,
                    fixture.Id,
                    yellowCardCount,
                    redCard,
                    playersOnField,
                    firstHalfStartedAtUtc,
                    firstHalfEndAtUtc,
                    secondHalfStartedAtUtc,
                    secondHalfEndAtUtc,
                    playerEvents);

                _dbContext.Teamsheets.Add(teamsheet);
                _dbContext.MatchEvents.AddRange(matchEvents);
                _dbContext.PlayerEvents.AddRange(playerEvents);
                _dbContext.SubstitutionEvents.AddRange(substitutionEvents);

                fixture.ChangeStatus(Status.Completed);
            }

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Finished seeding for match simulations.");
            return Result.Success();
        });
    }

    private static void SimulateHalf(
        Faker faker,
        Guid fixtureId,
        List<Player> playersOnField,
        DateTime halfStartUtc,
        DateTime halfEndUtc,
        DateTime firstHalfStartedAtUtc,
        DateTime? secondHalfStartedAtUtc,
        MatchPeriod period,
        List<PlayerEvent> playerEvents)
    {
        var currentUtc = halfStartUtc;

        while (currentUtc < halfEndUtc)
        {
            currentUtc = currentUtc.AddSeconds(faker.Random.Int(1, 45));

            if (currentUtc >= halfEndUtc) break;

            var player = faker.Random.ListItem(playersOnField);
            var eventType = MatchEventSelector.Select(faker, player.Position);
            var minute = MatchMinuteCalculator.Calculate(
                currentUtc, firstHalfStartedAtUtc, secondHalfStartedAtUtc, period);

            playerEvents.Add(PlayerEvent.Create(fixtureId, player.Id, eventType, minute, currentUtc));
        }
    }

    private static void AddGoalEvents(
        Faker faker,
        Fixture fixture,
        int homeGoals,
        int awayGoals,
        List<Player> playersOnField,
        DateTime firstHalfStartedAtUtc,
        DateTime firstHalfFinishedAtUtc,
        DateTime secondHalfStartedAtUtc,
        DateTime secondHalfFinishedAtUtc,
        List<PlayerEvent> playerEvents)
    {
        for (int i = 0; i < homeGoals; i++)
        {
            var ownGoal = faker.Random.Int(1, 20) == 20;
            var clubGoal = IsClubGoal(fixture.Location, true);

            if (clubGoal && !ownGoal)
            {
                // Note: sometimes there is no assist, so if random selects same player for both then that will result in no assist.
                var scorer = faker.Random.ListItem(playersOnField.Where(p => p.Position != Position.Goalkeeper).ToList());
                var assister = faker.Random.ListItem(playersOnField.Where(p => p.Position != Position.Goalkeeper).ToList());
                AddGoalEvent(
                    faker,
                    fixture.Id,
                    scorer,
                    assister == scorer ? null : assister,
                    firstHalfStartedAtUtc,
                    firstHalfFinishedAtUtc,
                    secondHalfStartedAtUtc,
                    secondHalfFinishedAtUtc,
                    playerEvents,
                    PlayerEventType.Goal);
            }
            else if (!clubGoal && ownGoal)
            {
                var scorer = faker.Random.ListItem(playersOnField);
                AddGoalEvent(
                    faker,
                    fixture.Id,
                    scorer,
                    null,
                    firstHalfStartedAtUtc,
                    firstHalfFinishedAtUtc,
                    secondHalfStartedAtUtc,
                    secondHalfFinishedAtUtc,
                    playerEvents,
                    PlayerEventType.OwnGoal);
            }
            else
            {
                // Opponent scored normally, no player event to record.
            }

            fixture.ChangeScore(Unwrap(Score.Create(fixture.Score.HomeGoals + 1, fixture.Score.AwayGoals)));
        }

        for (int i = 0; i < awayGoals; i++)
        {
            var ownGoal = faker.Random.Int(1, 20) == 20;
            var clubGoal = IsClubGoal(fixture.Location, false);

            if (clubGoal && !ownGoal)
            {
                // Note: sometimes there is no assist, so if random selects same player for both then that will result in no assist.
                var scorer = faker.Random.ListItem(playersOnField.Where(p => p.Position != Position.Goalkeeper).ToList());
                var assister = faker.Random.ListItem(playersOnField.Where(p => p.Position != Position.Goalkeeper).ToList());
                AddGoalEvent(
                    faker,
                    fixture.Id,
                    scorer,
                    assister == scorer ? null : assister,
                    firstHalfStartedAtUtc,
                    firstHalfFinishedAtUtc,
                    secondHalfStartedAtUtc,
                    secondHalfFinishedAtUtc,
                    playerEvents,
                    PlayerEventType.Goal);
            }
            else if (!clubGoal && ownGoal)
            {
                var scorer = faker.Random.ListItem(playersOnField);
                AddGoalEvent(
                    faker,
                    fixture.Id,
                    scorer,
                    null,
                    firstHalfStartedAtUtc,
                    firstHalfFinishedAtUtc,
                    secondHalfStartedAtUtc,
                    secondHalfFinishedAtUtc,
                    playerEvents,
                    PlayerEventType.OwnGoal);
            }
            else
            {
                // Opponent scored normally, no player event to record.
            }

            fixture.ChangeScore(Unwrap(Score.Create(fixture.Score.HomeGoals, fixture.Score.AwayGoals + 1)));
        }
    }

    private static void AddGoalEvent(
        Faker faker,
        Guid fixtureId,
        Player scorer,
        Player? assister,
        DateTime firstHalfStartedAtUtc,
        DateTime firstHalfFinishedAtUtc,
        DateTime secondHalfStartedAtUtc,
        DateTime secondHalfFinishedAtUtc,
        List<PlayerEvent> playerEvents,
        PlayerEventType playerEventType)
    {
        var occurredAtUtcAndPeriod = faker.Random.ListItem(
        [
            (faker.Date.Between(firstHalfStartedAtUtc, firstHalfFinishedAtUtc), MatchPeriod.FirstHalf),
            (faker.Date.Between(secondHalfStartedAtUtc, secondHalfFinishedAtUtc), MatchPeriod.SecondHalf),
        ]);

        var calculatedMinute = MatchMinuteCalculator.Calculate(
            occurredAtUtcAndPeriod.Item1,
            firstHalfStartedAtUtc,
            secondHalfStartedAtUtc,
            occurredAtUtcAndPeriod.Item2);

        if (playerEventType == PlayerEventType.Goal)
        {
            playerEvents.Add(
                PlayerEvent.Create(
                    fixtureId,
                    scorer.Id,
                    PlayerEventType.Goal,
                    calculatedMinute,
                    occurredAtUtcAndPeriod.Item1));

            if (assister is not null)
            {
                playerEvents.Add(
                    PlayerEvent.Create(
                        fixtureId,
                        assister.Id,
                        PlayerEventType.GoalAssist,
                        calculatedMinute,
                        occurredAtUtcAndPeriod.Item1));
            }
        }
        else if (playerEventType == PlayerEventType.OwnGoal)
        {
            playerEvents.Add(
                PlayerEvent.Create(
                    fixtureId,
                    scorer.Id,
                    PlayerEventType.OwnGoal,
                    calculatedMinute,
                    occurredAtUtcAndPeriod.Item1));
        }
    }

    private static void AddCardEvents(
        Faker faker,
        Guid fixtureId,
        int yellowCardCount,
        bool redCard,
        List<Player> playersOnField,
        DateTime firstHalfStartedAtUtc,
        DateTime firstHalfFinishedAtUtc,
        DateTime secondHalfStartedAtUtc,
        DateTime secondHalfFinishedAtUtc,
        List<PlayerEvent> playerEvents)
    {
        var cardedPlayerIds = new HashSet<Guid>();

        for (var i = 0; i < yellowCardCount; i++)
        {
            AddCardEvent(
            faker,
            fixtureId,
            playersOnField,
            firstHalfStartedAtUtc,
            firstHalfFinishedAtUtc,
            secondHalfStartedAtUtc,
            secondHalfFinishedAtUtc,
            playerEvents,
            PlayerEventType.YellowCard,
            cardedPlayerIds);
        }

        if (redCard)
        {
            AddCardEvent(
            faker,
            fixtureId,
            playersOnField,
            firstHalfStartedAtUtc,
            firstHalfFinishedAtUtc,
            secondHalfStartedAtUtc,
            secondHalfFinishedAtUtc,
            playerEvents,
            PlayerEventType.RedCard,
            cardedPlayerIds);
        }
    }

    private static void AddCardEvent(
        Faker faker,
        Guid fixtureId,
        List<Player> playersOnField,
        DateTime firstHalfStartedAtUtc,
        DateTime firstHalfFinishedAtUtc,
        DateTime secondHalfStartedAtUtc,
        DateTime secondHalfFinishedAtUtc,
        List<PlayerEvent> playerEvents,
        PlayerEventType playerEventType,
        HashSet<Guid> cardedPlayerIds)
    {
        var eligiblePlayers = playerEventType == PlayerEventType.YellowCard
            ? [.. playersOnField.Where(player => !cardedPlayerIds.Contains(player.Id))]
            : playersOnField;

        if (eligiblePlayers.Count == 0) return;

        var occurredAtUtcAndPeriod = faker.Random.ListItem(
        [
            (faker.Date.Between(firstHalfStartedAtUtc, firstHalfFinishedAtUtc), MatchPeriod.FirstHalf),
            (faker.Date.Between(secondHalfStartedAtUtc, secondHalfFinishedAtUtc), MatchPeriod.SecondHalf),
        ]);

        var player = faker.Random.ListItem(eligiblePlayers);

        if (playerEventType == PlayerEventType.YellowCard)
        {
            cardedPlayerIds.Add(player.Id);
        }

        var calculatedMinute = MatchMinuteCalculator.Calculate(
            occurredAtUtcAndPeriod.Item1,
            firstHalfStartedAtUtc,
            secondHalfStartedAtUtc,
            occurredAtUtcAndPeriod.Item2);

        playerEvents.Add(
            PlayerEvent.Create(
                fixtureId,
                player.Id,
                playerEventType,
                calculatedMinute,
                occurredAtUtcAndPeriod.Item1));
    }

    private static class MatchEventSelector
    {
        private static readonly PlayerEventType[] GoalkeeperEvents =
        [
            PlayerEventType.PassSuccess,
            PlayerEventType.PassFailure,
            PlayerEventType.Save
        ];

        private static readonly float[] GoalkeeperWeights = [0.50f, 0.10f, 0.40f];

        private static readonly PlayerEventType[] OutfielderEvents =
        [
            PlayerEventType.PassSuccess,
            PlayerEventType.PassFailure,
            PlayerEventType.ShotOnTarget,
            PlayerEventType.ShotOffTarget,
            PlayerEventType.Tackle,
            PlayerEventType.FoulWon,
            PlayerEventType.FoulConceded
        ];

        private static readonly float[] OutfielderWeights = [0.35f, 0.20f, 0.10f, 0.08f, 0.12f, 0.08f, 0.07f];

        public static PlayerEventType Select(Faker faker, Position position) =>
            position == Position.Goalkeeper
                ? faker.Random.WeightedRandom(GoalkeeperEvents, GoalkeeperWeights)
                : faker.Random.WeightedRandom(OutfielderEvents, OutfielderWeights);
    }

    private static bool IsClubGoal(Location location, bool isHomeGoal) => location == Location.Away ? !isHomeGoal : isHomeGoal;

    private static T Unwrap<T>(Result<T> result) =>
        result.IsSuccess
            ? result.Value
            : throw new InvalidOperationException($"Faker produced invalid data: {result.Error}");
}
