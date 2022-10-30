namespace OmahaDotDev.Model.Common
{
    public record SkipTakeSet<T>(List<T> Records)
    {
        public int Skipped { get; init; }
        public int Taken { get; init; }
        public int TotalRecords { get; init; }

    }


}
