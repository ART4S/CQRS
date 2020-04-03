using System;
using WebFeatures.Common;

namespace WebFeatures.Infrastructure.Common
{
    internal class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
