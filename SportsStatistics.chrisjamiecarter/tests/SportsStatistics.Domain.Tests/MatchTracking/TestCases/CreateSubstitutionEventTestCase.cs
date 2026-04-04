//using SportsStatistics.Domain.MatchTracking;
//using SportsStatistics.Domain.MatchTracking.MatchEvents;
//using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
//using SportsStatistics.Domain.Tests.Fixtures.TestData;
//using SportsStatistics.Domain.Tests.MatchTracking.TestData;

//namespace SportsStatistics.Domain.Tests.MatchTracking.TestCases;

//public class CreateSubstitutionEventTestCase : TheoryData<Guid, Substitution, Minute, DateTime>
//{
//    public CreateSubstitutionEventTestCase()
//    {
//        Add(FixtureTestData.ValidFixture.Id,
//            SubstitutionTestData.ValidSubstitution,
//            MinuteTestData.ValidMinute,
//            DateTime.UtcNow);
//    }
//}

//public class CreateMatchEventTestCase : TheoryData<Guid, MatchEventType, Minute, DateTime>
//{
//    public CreateMatchEventTestCase()
//    {
//        Add(FixtureTestData.ValidFixture.Id,
//            MatchEventTypeTestData.ValidMatchEventType,
//            MinuteTestData.ValidMinute,
//            DateTime.UtcNow);
//    }
//}
