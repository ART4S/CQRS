using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Infrastructure.Tests.Common.TestObjects
{
    public class TestEntity : Entity, IHasCreateDate
    {
        public int IntField;
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public TestValueObject ValueObjectProperty { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
