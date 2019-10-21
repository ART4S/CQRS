using System;

namespace WebFeatures.Common.Time
{
    internal class DefaultDateTimeProvider : DateTimeProvider
    {
        public override DateTime Now => DateTime.UtcNow;
    }
}
