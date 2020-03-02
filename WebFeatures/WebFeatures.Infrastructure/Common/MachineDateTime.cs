using System;
using WebFeatures.Common;

namespace WebFeatures.Infrastructure.Common
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
