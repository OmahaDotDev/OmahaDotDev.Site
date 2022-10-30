namespace Hero4Hire.TimeUtility
{
    class TimeUtility : ITimeUtility
    {
        public DateTime GetCurrentSystemTime()
        {
            return DateTime.Now;
        }

        public DateTime GetCurrentTimeZoneTime(string timeZone)
        {
            return DateTime.Now;
        }

        public IEnumerable<string> SearchTimeZones()
        {
            return new List<string>();
        }
    }
}