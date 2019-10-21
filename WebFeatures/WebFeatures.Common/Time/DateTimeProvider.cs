using System;

namespace WebFeatures.Common.Time
{
    public abstract class DateTimeProvider
    {
        public static DateTimeProvider Instance
        {
            get => _instance ??= new DefaultDateTimeProvider();
            set => _instance = value;
        }
        private static DateTimeProvider _instance;

        public abstract DateTime Now { get; }
    }
}
