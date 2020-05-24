using System;

namespace WebFeatures.Domian.Common
{
    public abstract class IdentityEntity : Entity
    {
        public Guid Id { get; set; }
    }
}
