namespace MovieStore.Api.Services
{
    public class BackgroundServiceOptions
    {
        public static string SectionName = "BackgroundService";
        public int ExpirationCheckIntervalMinutes { get; set; }

    }

}
