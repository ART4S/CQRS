using System.Collections.Generic;
using WebFeatures.Domian.Common;

namespace WebFeatures.Infrastructure.Tests.Common.Stubs
{
    public class CustomValueObject : ValueObject
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }

        protected override IEnumerable<object> GetComparisionValues()
        {
            yield return IntProperty;
            yield return StringProperty;
        }
    }
}