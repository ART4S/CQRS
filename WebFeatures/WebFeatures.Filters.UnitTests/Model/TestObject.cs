using System;

namespace WebFeatures.QueryFilters.Tests.Model
{
    internal class TestObject
    {
        public int IntValue { get; set; }

        public long LongValue { get; set; }

        public double DoubleValue { get; set; }

        public decimal DecimalValue { get; set; }

        public float FloatValue { get; set; }

        public bool BoolValue { get; set; }

        public string StringValue { get; set; }

        public Guid GuidValue { get; set; }

        public int? NullableIntValue { get; set; }

        public DateTime DateTimeValue { get; set; }

        public InnerObject InnerObject { get; set; }
    }
}
