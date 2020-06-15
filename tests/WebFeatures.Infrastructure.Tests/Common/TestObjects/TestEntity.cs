using WebFeatures.Domian.Common;

namespace WebFeatures.Infrastructure.Tests.Common.TestObjects
{
    internal class TestEntity : Entity
    {
        public int IntField;
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public TestValueObject ValueObjectProperty { get; set; }
    }
}
