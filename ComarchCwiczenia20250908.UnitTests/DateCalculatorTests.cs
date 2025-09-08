using ComarchCwiczenia20250908.Services;

namespace ComarchCwiczenia20250908.UnitTests;

[TestFixture]
public class DateCalculatorTests
{
    private DateCalculator cut = null!;

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

    [Test]
    public void GetNextBusinessDay_Should_SkipWeekends()
    {
        // Arrange
        DateTime friday = new DateTime(2025, 9, 5); 
        DateTime saturday = new DateTime(2025, 9, 6); 
        DateTime sunday = new DateTime(2025, 9, 7); 
        DateTime expected = new DateTime(2025, 9, 8);

        // Act
        DateTime resultForFriday = cut.GetNextBusinessDay(friday);
        DateTime resultForSaturday = cut.GetNextBusinessDay(saturday);
        DateTime resultForSunday = cut.GetNextBusinessDay(sunday);

        // Assert
        Assert.That(resultForFriday, Is.EqualTo(expected));
        Assert.That(resultForSaturday, Is.EqualTo(expected));
        Assert.That(resultForSunday, Is.EqualTo(expected));

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
}
