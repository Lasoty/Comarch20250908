using ComarchCwiczenia20250908.Services;
using Shouldly;

namespace ComarchCwiczenia20250908.UnitTests;

[TestFixture]
public class DateCalculatorShouldlyTests
{
    private DateCalculator cut;

    [SetUp]
    public void Setup()
    {
        cut = new DateCalculator();
    }

    [Test]
    public void GetNextBusinessDay_Should_SkipWeekends()
    {
        cut.GetNextBusinessDay(new DateTime(2025,9,5)).ShouldBe(new DateTime(2025,9,8));
        cut.GetNextBusinessDay(new DateTime(2025,9,6)).ShouldBe(new DateTime(2025,9,8));
        cut.GetNextBusinessDay(new DateTime(2025,9,7)).ShouldBe(new DateTime(2025,9,8));
    }

    [Test]
    public void GetNextBusinessDay_Should_BeAfterInputDate()
    {
        var date = new DateTime(2025, 9, 8);
        cut.GetNextBusinessDay(date).ShouldBeGreaterThan(date);
    }

}
