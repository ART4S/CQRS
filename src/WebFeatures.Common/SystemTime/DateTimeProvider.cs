namespace WebFeatures.Common.SystemTime
{
    public static class DateTimeProvider
    {
        public static IDateTime DateTime
        {
            get => _dateTime ??= new DefaultDateTime();
            internal set => _dateTime = value;
        }
        private static IDateTime _dateTime;
    }
}
