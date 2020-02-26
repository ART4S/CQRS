using System.Collections.Generic;
using System.Linq;

namespace WebFeatures.Domian.Infrastructure
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object other)
        {
            if (other == null || other.GetType() != GetType())
            {
                return false;
            }

            using var thisEnumerator = GetAtomicValues().GetEnumerator();
            using var otherEnumerator = ((ValueObject) other).GetAtomicValues().GetEnumerator();

            while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
            {
                if (!(thisEnumerator.Current is null) && !thisEnumerator.Current.Equals(otherEnumerator.Current))
                {
                    return false;
                }

                if (!(otherEnumerator.Current is null) && !otherEnumerator.Current.Equals(thisEnumerator.Current))
                {
                    return false;
                }
            }

            return !thisEnumerator.MoveNext() && !thisEnumerator.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x == null ? 0 : x.GetHashCode())
                .Aggregate((hash, current) => hash ^ current);
        }
    }
}
