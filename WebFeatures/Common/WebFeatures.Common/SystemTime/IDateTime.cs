using System;

namespace WebFeatures.Common.SystemTime
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }

    internal class DefaultDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
