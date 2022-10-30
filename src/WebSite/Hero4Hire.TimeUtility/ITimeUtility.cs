namespace Hero4Hire.TimeUtility
{
    public interface ITimeUtility
    {
        DateTime GetCurrentSystemTime();
        IEnumerable<string> SearchTimeZones();
        DateTime GetCurrentTimeZoneTime(string timeZone);

    }
}
