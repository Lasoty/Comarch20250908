using ComarchCwiczenia20250908.Services;

namespace ComarchCwiczenia20250908.UnitTests;

[TestFixture]
public class DateCalculatorTests
{
    private DateCalculator cut = null!;

    private static object[] DateTimes =
    [
        new object[] {new DateTime(2025,09,05), new DateTime(2025, 09, 08) },
        new object[] {new DateTime(2025,09,06), new DateTime(2025, 09, 08) },
        new object[] {new DateTime(2025,09,07), new DateTime(2025, 09, 08) }
    ];

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Console.WriteLine("OneTimeSetUp");
    }

    [SetUp]
    public void Setup()
    {
        cut = new DateCalculator();
    }

    [TestCase("2025-09-05 00:00:00", "2025-09-08 00:00:00")]
    [TestCase("2025-09-06 00:00:00", "2025-09-08 00:00:00")]
    [TestCase("2025-09-07 00:00:00", "2025-09-08 00:00:00")]
    public void GetNextBusinessDay_Should_SkipWeekends(string sDate, string sExpected)
    {
        // Arrange
        DateTime inputData = DateTime.Parse(sDate); 
        DateTime expected = DateTime.Parse(sExpected); 

        // Act
        DateTime actual = cut.GetNextBusinessDay(inputData);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCaseSource(nameof(DateTimes))]
    public void GetNextBusinessDay_Should_SkipWeekends2(DateTime inputData, DateTime expected)
    {
        // Act
        DateTime actual = cut.GetNextBusinessDay(inputData);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextBusinessDay_ShouldHandleLeapYear()
    {
        DateTime inputDay = new DateTime(2024, 2, 28);
        DateTime expected = new DateTime(2024, 2, 29);

        DateTime actual = cut.GetNextBusinessDay(inputDay);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextBusinessDay_ShouldNotChangeTimePart()
    {
        var time = new DateTime(2024, 2, 23, 15, 30, 0); // 15:30

        DateTime actual = cut.GetNextBusinessDay(time);

        Assert.That(actual.TimeOfDay, Is.EqualTo(time.TimeOfDay));
    }

    [Test]
    public void GetNextBusinessDay_ShouldBeAfterInputDate()
    {
        var date = new DateTime(2024, 2, 23);

        DateTime actual = cut.GetNextBusinessDay(date);

        Assert.That(actual > date, Is.True);
    }

    [TearDown]
    public void TearDown()
    {
        Console.WriteLine("TearDown");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Console.WriteLine("OneTimeTearDown");
    }



    [Test]
    public void WeekDaysInMonth_Should_ReturnsCorrectWeekendDays()
    {
        int month = 9;

        IEnumerable<DateTime> actual = cut.WeekDaysInMonth(month, 2025);

        Assert.That(actual, Does.Contain(new DateTime(2025, 9, 6)));
        Assert.That(actual, Does.Contain(new DateTime(2025,9,28)));
        Assert.That(actual, Is.All.GreaterThan(
            new DateTime(2025, 9, 1))
            .And.All.LessThanOrEqualTo(new DateTime(2025, 9, 28)));

        Assert.That(actual, Has.Exactly(1).EqualTo(new DateTime(2025, 9, 6)));
    }
}
