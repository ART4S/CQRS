using System.Collections.Generic;
using WebFeatures.Domian.Common;

namespace WebFeatures.Infrastructure.Tests.Helpers.TestObjects
{
    public class TestValueObject : ValueObject
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