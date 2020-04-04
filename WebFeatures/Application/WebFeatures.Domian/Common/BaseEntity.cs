using System;
using System.Collections.Generic;
using WebFeatures.Events;

namespace WebFeatures.Domian.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public ICollection<IEvent> Events { get; private set; } = new HashSet<IEvent>();
    }
}
