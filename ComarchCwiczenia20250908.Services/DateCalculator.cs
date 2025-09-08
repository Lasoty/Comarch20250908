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

    public IEnumerable<DateTime> WeekDaysInMonth(int month, int year)
    {
        List<DateTime> daysInMonth = [];
        var date = new DateTime(year, month, 1);
        while (date.Month == month)
        {
            if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                daysInMonth.Add(date);
            date = date.AddDays(1);
        }
        return daysInMonth;
    }
}