//using SportsStatistics.Domain.MatchTracking;

//namespace SportsStatistics.Domain.Tests.MatchTracking.TestData;

//public static class MinuteTestData
//{
//    public static Minute ValidMinute => Minute.Create(Minute.MinBaseMinute).Value;

//    public static readonly int BelowMinValueMinute = Minute.MinBaseMinute - 1;

//    public static readonly int? NullMinute;

//    /// <summary>
//    /// Creates a valid minute for first half (minute 1-45).
//    /// </summary>
//    public static Minute FirstHalfMinute(int minute = 1) => Minute.FirstHalfMinute(minute).Value;

//    /// <summary>
//    /// Creates a valid minute for second half (minute 46-90).
//    /// </summary>
//    public static Minute SecondHalfMinute(int minute = 46) => Minute.SecondHalfMinute(minute).Value;

//    /// <summary>
//    /// Creates a valid minute for first half stoppage time (45+1, etc.).
//    /// </summary>
//    public static Minute FirstHalfStoppage(int stoppageMinute = 1) => Minute.FirstHalfStoppage(stoppageMinute).Value;

//    /// <summary>
//    /// Creates a valid minute for second half stoppage time (90+1, etc.).
//    /// </summary>
//    public static Minute SecondHalfStoppage(int stoppageMinute = 1) => Minute.SecondHalfStoppage(stoppageMinute).Value;
//}
