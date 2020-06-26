using System;
using WebFeatures.Domian.Common;

namespace WebFeatures.Infrastructure.Tests.Common.Stubs
{
    public class CustomEntity : Entity, IHasCreateDate
    {
        public int IntField;
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public CustomValueObject ValueObjectProperty { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
