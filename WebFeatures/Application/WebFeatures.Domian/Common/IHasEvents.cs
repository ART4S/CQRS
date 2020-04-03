using System.Collections.Generic;
using WebFeatures.Events;

namespace WebFeatures.Domian.Common
{
    public interface IHasEvents
    {
        ICollection<IEvent> Events { get; }
    }
}
