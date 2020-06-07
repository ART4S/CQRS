using System;
using WebFeatures.Common;

namespace WebFeatures.Common.SystemTime
{
    internal class DefaultDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
