using System.Collections.Generic;
using WebFeatures.Events;

namespace WebFeatures.Domian.Common
{
    public abstract class Entity
    {
        public ICollection<IEvent> Events { get; private set; } = new HashSet<IEvent>();
    }
}
