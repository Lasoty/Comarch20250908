namespace ComarchCwiczenia20250908.Services;

public class DateCalculator
{
    public DateTime GetNextBusinessDay(DateTime date)
    {
        do
        {
            date = date.AddDays(1);
        } while (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday);

        return date;
    }
}