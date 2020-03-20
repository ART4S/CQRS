using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using WebFeatures.Domian.Events;

namespace WebFeatures.Domian.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<DomianEvent> EventsList => Events.ToImmutableList();
        protected readonly HashSet<DomianEvent> Events = new HashSet<DomianEvent>();
    }
}
