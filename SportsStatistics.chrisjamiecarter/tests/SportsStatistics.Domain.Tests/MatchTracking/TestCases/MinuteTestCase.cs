//using SportsStatistics.Domain.MatchTracking;
//using Xunit;

//namespace SportsStatistics.Domain.Tests.MatchTracking.TestCases;

///// <summary>
///// Test cases for valid minute creation across all periods.
///// </summary>
//public class ValidMinuteTestCase : TheoryData<int, int?, string, MatchPeriod>
//{
//    public ValidMinuteTestCase()
//    {
//        // First Half regulation time (minutes 1-45)
//        Add(1, null, "1", MatchPeriod.FirstHalf);
//        Add(30, null, "30", MatchPeriod.FirstHalf);
//        Add(45, null, "45", MatchPeriod.FirstHalf);

//        // First Half stoppage time (45+1, 45+2, etc.)
//        Add(45, 1, "45+1", MatchPeriod.FirstHalfStoppage);
//        Add(45, 2, "45+2", MatchPeriod.FirstHalfStoppage);
//        Add(45, 5, "45+5", MatchPeriod.FirstHalfStoppage);

//        // Second Half regulation time (minutes 46-90)
//        Add(46, null, "46", MatchPeriod.SecondHalf);
//        Add(60, null, "60", MatchPeriod.SecondHalf);
//        Add(90, null, "90", MatchPeriod.SecondHalf);

//        // Second Half stoppage time (90+1, 90+2, etc.)
//        Add(90, 1, "90+1", MatchPeriod.SecondHalfStoppage);
//        Add(90, 3, "90+3", MatchPeriod.SecondHalfStoppage);
//        Add(90, 7, "90+7", MatchPeriod.SecondHalfStoppage);
//    }
//}

///// <summary>
///// Test cases for invalid minute creation.
///// </summary>
//public class InvalidMinuteTestCase : TheoryData<int?, int?, Type>
//{
//    public InvalidMinuteTestCase()
//    {
//        // Null minute (BR-007)
//        Add(null, null, typeof(NullReferenceException));

//        // Below minimum (BR-007: minimum is 1)
//        Add(0, null, typeof(InvalidOperationException));
//        Add(-1, null, typeof(InvalidOperationException));

//        // Above maximum
//        Add(121, null, typeof(InvalidOperationException));

//        // Invalid stoppage minute (must be >= 1)
//        Add(45, 0, typeof(InvalidOperationException));
//        Add(45, -1, typeof(InvalidOperationException));

//        // Invalid base minute for stoppage (must be 45, 90, 105, or 120)
//        Add(44, 1, typeof(InvalidOperationException));
//        Add(46, 1, typeof(InvalidOperationException));
//        Add(89, 1, typeof(InvalidOperationException));
//        Add(91, 1, typeof(InvalidOperationException));
//    }
//}

///// <summary>
///// Test cases for FirstHalfMinute factory method.
///// </summary>
//public class FirstHalfMinuteTestCase : TheoryData<int, bool>
//{
//    public FirstHalfMinuteTestCase()
//    {
//        // Valid first half minutes (BR-003)
//        Add(1, true);
//        Add(45, true);
//        Add(30, true);

//        // Invalid first half minutes
//        Add(0, false);   // Below minimum
//        Add(46, false);  // Second half
//        Add(90, false);  // Second half
//    }
//}

///// <summary>
///// Test cases for SecondHalfMinute factory method.
///// </summary>
//public class SecondHalfMinuteTestCase : TheoryData<int, bool>
//{
//    public SecondHalfMinuteTestCase()
//    {
//        // Valid second half minutes (BR-004)
//        Add(46, true);
//        Add(90, true);
//        Add(67, true);

//        // Invalid second half minutes
//        Add(45, false);  // First half (BR-004: minute 45 cannot be assigned to second-half)
//        Add(1, false);   // First half
//        Add(91, false);  // Extra time
//        Add(0, false);   // Below minimum
//    }
//}

///// <summary>
///// Test cases for FromElapsedSeconds factory method (BR-002).
///// </summary>
//public class ElapsedSecondsTestCase : TheoryData<int, MatchPeriod, int, int?, string>
//{
//    public ElapsedSecondsTestCase()
//    {
//        // Recorded_minute = floor(elapsed_seconds / 60) + 1

//        // First Half
//        Add(0, MatchPeriod.FirstHalf, 1, null, "1");        // 0 seconds = minute 1
//        Add(59, MatchPeriod.FirstHalf, 1, null, "1");      // 59 seconds = minute 1
//        Add(60, MatchPeriod.FirstHalf, 2, null, "2");      // 60 seconds = minute 2
//        Add(2699, MatchPeriod.FirstHalf, 45, null, "45");  // 44:59 = minute 45

//        // First Half Stoppage
//        Add(0, MatchPeriod.FirstHalfStoppage, 45, 1, "45+1");
//        Add(60, MatchPeriod.FirstHalfStoppage, 45, 2, "45+2");
//        Add(119, MatchPeriod.FirstHalfStoppage, 45, 2, "45+2");
//        Add(120, MatchPeriod.FirstHalfStoppage, 45, 3, "45+3");

//        // Second Half
//        Add(0, MatchPeriod.SecondHalf, 46, null, "46");    // Start of second half
//        Add(59, MatchPeriod.SecondHalf, 46, null, "46");
//        Add(60, MatchPeriod.SecondHalf, 47, null, "47");
//        Add(2699, MatchPeriod.SecondHalf, 90, null, "90"); // 44:59 into second half = minute 90

//        // Second Half Stoppage
//        Add(0, MatchPeriod.SecondHalfStoppage, 90, 1, "90+1");
//        Add(60, MatchPeriod.SecondHalfStoppage, 90, 2, "90+2");
//    }
//}

///// <summary>
///// Test cases for period-specific factory methods.
///// </summary>
//public class PeriodSpecificMinuteTestCase : TheoryData<int, MatchPeriod, bool>
//{
//    public PeriodSpecificMinuteTestCase()
//    {
//        // First Half Stoppage
//        Add(1, MatchPeriod.FirstHalfStoppage, true);
//        Add(5, MatchPeriod.FirstHalfStoppage, true);

//        // Second Half Stoppage
//        Add(1, MatchPeriod.SecondHalfStoppage, true);
//        Add(3, MatchPeriod.SecondHalfStoppage, true);
//    }
//}
