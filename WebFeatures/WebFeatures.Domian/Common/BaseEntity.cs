using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using WebFeatures.Domian.Events;

namespace WebFeatures.Domian.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<IDomianEvent> Events => _events.ToImmutableList();
        private readonly HashSet<IDomianEvent> _events = new HashSet<IDomianEvent>();

        protected void AddEvent(IDomianEvent eve)
        {
            _events.Add(eve);
        }
    }
}
