using System.Collections.Generic;
using WebFeatures.Events;

namespace WebFeatures.Domian.Events
{
    public interface IHasEvents
    {
        ICollection<IEvent> Events { get; }
    }
}
