using System;
using System.Collections.Generic;
using WebFeatures.Domian.Events;

namespace WebFeatures.Domian.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public IReadOnlyCollection<IDomianEvent> Events => _events.AsReadOnly();
        private readonly List<IDomianEvent> _events = new List<IDomianEvent>();

        protected void AddEvent(IDomianEvent eve)
        {
            _events.Add(eve);
        }
    }
}
