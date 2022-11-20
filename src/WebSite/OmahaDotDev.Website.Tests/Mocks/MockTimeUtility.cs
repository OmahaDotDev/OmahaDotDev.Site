using Hero4Hire.TimeUtility;

namespace OmahaDotDev.Website.Tests.Mocks;

public class MockTimeUtility : ITimeUtility
{
    private DateTime _currentDateTime;
    public MockTimeUtility()
    {
        _currentDateTime = DateTime.Now;
    }

    public void RestTime()
    {
        _currentDateTime = DateTime.Now;
    }

    public void ChangeTime(int hours, int days = 0, int months = 0, int years = 0)
    {
        _currentDateTime = _currentDateTime
            .AddHours(hours)
            .AddDays(days)
            .AddMonths(months)
            .AddYears(years);
    }
    public DateTime GetCurrentSystemTime()
    {
        return _currentDateTime;
    }

    public IEnumerable<string> SearchTimeZones()
    {
        throw new NotImplementedException();
    }

    public DateTime GetCurrentTimeZoneTime(string timeZone)
    {
        throw new NotImplementedException();
    }
}