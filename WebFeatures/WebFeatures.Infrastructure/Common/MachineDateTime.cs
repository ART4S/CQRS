using System;
using WebFeatures.Common.Time;

namespace WebFeatures.Infrastructure.Common
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
