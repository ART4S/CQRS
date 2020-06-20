using System;

namespace WebFeatures.Common
{
    public static class SystemTime
    {
        internal static ISystemTime DateTime
        {
            get => _dateTime ??= new DefaultSystemTime();
            set => _dateTime = value;
        }
        private static ISystemTime _dateTime;

        public static DateTime Now => DateTime.Now;

        internal interface ISystemTime
        {
            DateTime Now { get; }
        }

        private class DefaultSystemTime : ISystemTime
        {
            public DateTime Now => DateTime.Now;
        }
    }
}