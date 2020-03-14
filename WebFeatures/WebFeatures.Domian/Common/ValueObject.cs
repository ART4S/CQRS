using System.Collections.Generic;
using System.Linq;

namespace WebFeatures.Domian.Common
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetComparisionValues();

        public override bool Equals(object other)
        {
            if (other == null) return false;
            if (other.GetType() != GetType()) return false;
            if (ReferenceEquals(other, this)) return true;

            return ((ValueObject)other).GetComparisionValues().SequenceEqual(GetComparisionValues());
        }

        public override int GetHashCode()
        {
            return GetComparisionValues()
                .Aggregate(0, (hash, current) => hash ^ (current == null ? 0 : current.GetHashCode()));
        }
    }
}
