using System.Collections.Generic;

namespace WebFeatures.Domian.Events
{
    public interface IHasDomianEvents
    {
        IReadOnlyCollection<IDomianEvent> Events { get; }
    }
}
