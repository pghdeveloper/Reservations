namespace Reservations.API.Validators;

public static class DateValidator
{
    public static bool HaveTimeComponent(DateTime date)
    {
        return date.TimeOfDay != TimeSpan.Zero;
    }
    
    public static bool BeInFifteenMinuteInterval(DateTime date)
    {
        int totalMinutes = (int)date.TimeOfDay.TotalMinutes;
        return totalMinutes % 15 == 0;
    }
}