//using SportsStatistics.Domain.MatchTracking;
//using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
//using SportsStatistics.Domain.Tests.MatchTracking.TestCases;

//namespace SportsStatistics.Domain.Tests.MatchTracking;

//public class SubstitutionEventTests
//{
//    [Theory]
//    [ClassData(typeof(CreateSubstitutionEventTestCase))]
//    public void Create_ShouldCreateSubstitutionEvent_WhenParametersAreValid(Guid fixtureId, Substitution substitution, Minute minute, DateTime occurredAtUtc)
//    {
//        // Arrange.
//        // Act.
//        var substitutionEvent = SubstitutionEvent.Create(fixtureId, substitution, minute, occurredAtUtc);

//        // Assert.
//        substitutionEvent.ShouldNotBeNull();
//        substitutionEvent.Id.ShouldNotBe(default);
//        substitutionEvent.Id.Version.ShouldBe(7);
//        substitutionEvent.FixtureId.ShouldBeEquivalentTo(fixtureId);
//        substitutionEvent.Substitution.ShouldBeEquivalentTo(substitution);
//        substitutionEvent.Minute.ShouldBeEquivalentTo(minute);
//        substitutionEvent.OccurredAtUtc.ShouldBeEquivalentTo(occurredAtUtc);
//        substitutionEvent.Deleted.ShouldBeFalse();
//        substitutionEvent.DeletedOnUtc.ShouldBeNull();
//    }
//}
