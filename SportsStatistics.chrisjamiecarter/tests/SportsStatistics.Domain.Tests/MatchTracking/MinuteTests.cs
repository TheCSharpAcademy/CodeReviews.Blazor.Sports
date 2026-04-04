//using SportsStatistics.Domain.MatchTracking;
//using SportsStatistics.Domain.Tests.MatchTracking.TestCases;
//using Shouldly;

//namespace SportsStatistics.Domain.Tests.MatchTracking;

//public class MinuteTests
//{
//    #region Create Method Tests

//    [Theory]
//    [ClassData(typeof(ValidMinuteTestCase))]
//    public void Create_ShouldCreateMinute_WhenParametersAreValid(
//        int baseMinute,
//        int? stoppageMinute,
//        string expectedDisplay)
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.Create(baseMinute, stoppageMinute);

//        // Assert.
//        result.IsSuccess.ShouldBeTrue();
//        var minute = result.Value;
//        minute.BaseMinute.ShouldBe(baseMinute);
//        minute.StoppageMinute.ShouldBe(stoppageMinute);
//        minute.Display.ShouldBe(expectedDisplay);
//    }

//    [Theory]
//    [ClassData(typeof(InvalidMinuteTestCase))]
//    public void Create_ShouldReturnFailure_WhenParametersAreInvalid(
//        int? baseMinute,
//        int? stoppageMinute,
//        Type _)
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.Create(baseMinute, stoppageMinute);

//        // Assert.
//        result.IsFailure.ShouldBeTrue();
//    }

//    [Fact]
//    public void Create_ShouldReturnBelowMinimum_WhenBaseMinuteIsZero()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.Create(0);

//        // Assert.
//        result.IsFailure.ShouldBeTrue();
//        result.Error.Code.ShouldBe("MatchTracking.Minute.BelowMinimum");
//    }

//    [Fact]
//    public void Create_ShouldReturnNull_WhenBaseMinuteIsNull()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.Create(null);

//        // Assert.
//        result.IsFailure.ShouldBeTrue();
//        result.Error.Code.ShouldBe("MatchTracking.Minute.Null");
//    }

//    [Fact]
//    public void Create_ShouldReturnAboveMaximum_WhenBaseMinuteExceeds120()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.Create(121);

//        // Assert.
//        result.IsFailure.ShouldBeTrue();
//        result.Error.Code.ShouldBe("MatchTracking.Minute.AboveMaximum");
//    }

//    #endregion

//    #region FirstHalfMinute Tests

//    [Theory]
//    [ClassData(typeof(FirstHalfMinuteTestCase))]
//    public void FirstHalfMinute_ShouldValidateCorrectly(int minute, bool shouldSucceed)
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.FirstHalfMinute(minute);

//        // Assert.
//        if (shouldSucceed)
//        {
//            result.IsSuccess.ShouldBeTrue();
//            result.Value.Period.ShouldBe(MatchPeriod.FirstHalf);
//            result.Value.StoppageMinute.ShouldBeNull();
//        }
//        else
//        {
//            result.IsFailure.ShouldBeTrue();
//        }
//    }

//    [Fact]
//    public void FirstHalfMinute_ShouldCreateMinute1_WhenMinimumValidMinute()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.FirstHalfMinute(1);

//        // Assert.
//        result.IsSuccess.ShouldBeTrue();
//        result.Value.BaseMinute.ShouldBe(1);
//        result.Value.Display.ShouldBe("1");
//    }

//    [Fact]
//    public void FirstHalfMinute_ShouldCreateMinute45_WhenMaximumValidMinute()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.FirstHalfMinute(45);

//        // Assert.
//        result.IsSuccess.ShouldBeTrue();
//        result.Value.BaseMinute.ShouldBe(45);
//        result.Value.Display.ShouldBe("45");
//    }

//    #endregion

//    #region FirstHalfStoppage Tests

//    [Fact]
//    public void FirstHalfStoppage_ShouldCreate45Plus1_WhenStoppageMinute1()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.FirstHalfStoppage(1);

//        // Assert.
//        result.IsSuccess.ShouldBeTrue();
//        result.Value.BaseMinute.ShouldBe(45);
//        result.Value.StoppageMinute.ShouldBe(1);
//        result.Value.Display.ShouldBe("45+1");
//        result.Value.Period.ShouldBe(MatchPeriod.FirstHalfStoppage);
//    }

//    [Fact]
//    public void FirstHalfStoppage_ShouldReturnFailure_WhenStoppageMinuteIsZero()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.FirstHalfStoppage(0);

//        // Assert.
//        result.IsFailure.ShouldBeTrue();
//        result.Error.Code.ShouldBe("MatchTracking.Minute.InvalidStoppageMinute");
//    }

//    #endregion

//    #region SecondHalfMinute Tests

//    [Theory]
//    [ClassData(typeof(SecondHalfMinuteTestCase))]
//    public void SecondHalfMinute_ShouldValidateCorrectly(int minute, bool shouldSucceed)
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.SecondHalfMinute(minute);

//        // Assert.
//        if (shouldSucceed)
//        {
//            result.IsSuccess.ShouldBeTrue();
//            result.Value.Period.ShouldBe(MatchPeriod.SecondHalf);
//            result.Value.StoppageMinute.ShouldBeNull();
//        }
//        else
//        {
//            result.IsFailure.ShouldBeTrue();
//        }
//    }

//    [Fact]
//    public void SecondHalfMinute_ShouldReturnInvalidForSecondHalf_WhenMinuteIs45()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.SecondHalfMinute(45);

//        // Assert.
//        result.IsFailure.ShouldBeTrue();
//        result.Error.Code.ShouldBe("MatchTracking.Minute.InvalidForSecondHalf");
//    }

//    [Fact]
//    public void SecondHalfMinute_ShouldCreateMinute46_WhenMinimumValidMinute()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.SecondHalfMinute(46);

//        // Assert.
//        result.IsSuccess.ShouldBeTrue();
//        result.Value.BaseMinute.ShouldBe(46);
//        result.Value.Display.ShouldBe("46");
//    }

//    #endregion

//    #region SecondHalfStoppage Tests

//    [Fact]
//    public void SecondHalfStoppage_ShouldCreate90Plus1_WhenStoppageMinute1()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.SecondHalfStoppage(1);

//        // Assert.
//        result.IsSuccess.ShouldBeTrue();
//        result.Value.BaseMinute.ShouldBe(90);
//        result.Value.StoppageMinute.ShouldBe(1);
//        result.Value.Display.ShouldBe("90+1");
//        result.Value.Period.ShouldBe(MatchPeriod.SecondHalfStoppage);
//    }

//    [Fact]
//    public void SecondHalfStoppage_ShouldCreate90Plus5_WhenStoppageMinute5()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.SecondHalfStoppage(5);

//        // Assert.
//        result.IsSuccess.ShouldBeTrue();
//        result.Value.Display.ShouldBe("90+5");
//    }

//    #endregion

//    #region FromElapsedSeconds Tests

//    [Theory]
//    [ClassData(typeof(ElapsedSecondsTestCase))]
//    public void FromElapsedSeconds_ShouldCalculateCorrectMinute(
//        int elapsedSeconds,
//        MatchPeriod period,
//        int expectedBaseMinute,
//        int? expectedStoppageMinute,
//        string expectedDisplay)
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.FromElapsedSeconds(elapsedSeconds, period);

//        // Assert.
//        result.IsSuccess.ShouldBeTrue();
//        result.Value.BaseMinute.ShouldBe(expectedBaseMinute);
//        result.Value.StoppageMinute.ShouldBe(expectedStoppageMinute);
//        result.Value.Display.ShouldBe(expectedDisplay);
//    }

//    [Fact]
//    public void FromElapsedSeconds_ShouldReturnBelowMinimum_WhenSecondsAreNegative()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.FromElapsedSeconds(-1, MatchPeriod.FirstHalf);

//        // Assert.
//        result.IsFailure.ShouldBeTrue();
//        result.Error.Code.ShouldBe("MatchTracking.Minute.BelowMinimum");
//    }

//    [Fact]
//    public void FromElapsedSeconds_ShouldReturnFailure_WhenPeriodIsNotPlayingPeriod()
//    {
//        // Arrange.
//        // Act.
//        var result = Minute.FromElapsedSeconds(0, MatchPeriod.HalfTime);

//        // Assert.
//        result.IsFailure.ShouldBeTrue();
//    }

//    #endregion

//    #region ToTotalSeconds Tests

//    [Fact]
//    public void ToTotalSeconds_ShouldReturnCorrectValue_ForRegulationMinute()
//    {
//        // Arrange.
//        var minute = Minute.Create(45).Value;

//        // Act.
//        var totalSeconds = minute.ToTotalSeconds();

//        // Assert.
//        totalSeconds.ShouldBe(45 * 60);
//    }

//    [Fact]
//    public void ToTotalSeconds_ShouldReturnCorrectValue_ForStoppageMinute()
//    {
//        // Arrange.
//        var minute = Minute.Create(45, 2).Value;

//        // Act.
//        var totalSeconds = minute.ToTotalSeconds();

//        // Assert.
//        totalSeconds.ShouldBe(45 * 60 + (2 * 60));
//    }

//    [Fact]
//    public void ToTotalSeconds_ShouldOrderCorrectly_WhenComparingStoppageMinutes()
//    {
//        // Arrange.
//        var regulation = Minute.Create(45).Value;
//        var stoppage1 = Minute.Create(45, 1).Value;
//        var stoppage2 = Minute.Create(45, 2).Value;

//        // Act & Assert.
//        regulation.ToTotalSeconds().ShouldBeLessThan(stoppage1.ToTotalSeconds());
//        stoppage1.ToTotalSeconds().ShouldBeLessThan(stoppage2.ToTotalSeconds());
//    }

//    #endregion

//    #region Implicit Conversion Tests

//    [Fact]
//    public void ImplicitConversion_ShouldReturnBaseMinute_WhenConvertedToInt()
//    {
//        // Arrange.
//        var minute = Minute.Create(45).Value;

//        // Act.
//        int value = minute;

//        // Assert.
//        value.ShouldBe(45);
//    }

//    [Fact]
//    public void ImplicitConversion_ShouldThrow_WhenMinuteIsNull()
//    {
//        // Arrange.
//        Minute? minute = null;

//        // Act & Assert.
//        Should.Throw<ArgumentNullException>(() => { int _ = minute; });
//    }

//    #endregion

//    #region Display Notation Tests

//    [Theory]
//    [InlineData(1, null, "1")]
//    [InlineData(45, null, "45")]
//    [InlineData(45, 1, "45+1")]
//    [InlineData(45, 5, "45+5")]
//    [InlineData(90, null, "90")]
//    [InlineData(90, 3, "90+3")]
//    [InlineData(105, 2, "105+2")]
//    [InlineData(120, 1, "120+1")]
//    public void DisplayNotation_ShouldReturnCorrectFormat(int baseMinute, int? stoppage, string expected)
//    {
//        // Arrange.
//        var minute = Minute.Create(baseMinute, stoppage).Value;

//        // Act.
//        var display = minute.Display;

//        // Assert.
//        display.ShouldBe(expected);
//    }

//    #endregion
//}
