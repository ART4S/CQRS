using System;
using System.Linq;
using WebFeatures.QueryFilters.Tests.Model;
using Xunit;

namespace WebFeatures.QueryFilters.Tests
{
    public class QueryFiltersTests
    {
        [Fact]
        public void ApplyQuery_EmptyQuery_ReturnsSameQueryable()
        {
            var testObjects = new[]
            {
                new TestObject()
            }.AsQueryable();

            var expected = testObjects;
            var actual = testObjects.ApplyQuery("");

            Assert.Same(expected, actual);
        }

        #region Top

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-1)]
        public void Top_Count_ReturnsOne(int count)
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            var expected = testObjects.Take(count);
            var actual = testObjects.ApplyQuery($"$top={count}");

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Skip

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-1)]
        public void Skip_Count_ReturnsOne(int count)
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            var expected = testObjects.Skip(count);
            var actual = testObjects.ApplyQuery($"$skip={count}");

            Assert.Equal(expected, actual);
        }

        #endregion

        #region OrderBy

        [Fact]
        public void OrderBy_IntValueByAsc_ReturnsOrdered()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = 3},
                new TestObject(){IntValue = 2},
                new TestObject(){IntValue = 1},
            }.AsQueryable();

            var expected = testObjects.OrderBy(x => x.IntValue);
            var actual = testObjects.ApplyQuery("$orderBy=IntValue");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderBy_IntValueByAscWithDifferentSyntax_ReturnsOrdered()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = 3},
                new TestObject(){IntValue = 2},
                new TestObject(){IntValue = 1}
            }.AsQueryable();

            var expected = testObjects.ApplyQuery("$orderBy=IntValue");
            var actual = testObjects.ApplyQuery("$orderBy=IntValue asc");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderBy_IntValueByDesc_ReturnsOrdered()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = 1},
                new TestObject(){IntValue = 2},
                new TestObject(){IntValue = 3},
            }.AsQueryable();

            var expected = testObjects.OrderByDescending(x => x.IntValue);
            var actual = testObjects.ApplyQuery("$orderBy=IntValue desc");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderBy_IntValueByAscThenDoubleValueByDesc_ReturnsOrdered()
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = 4, DoubleValue = 1},
                new TestObject(){IntValue = 3, DoubleValue = 6},
                new TestObject(){IntValue = 2, DoubleValue = 4},
                new TestObject(){IntValue = 2, DoubleValue = 3},
                new TestObject(){IntValue = 2, DoubleValue = 5},
                new TestObject(){IntValue = 2, DoubleValue = 2},
                new TestObject(){IntValue = 1, DoubleValue = 7},
            }.AsQueryable();

            var expected = testObjects.OrderBy(x => x.IntValue).ThenByDescending(x => x.DoubleValue);
            var actual = testObjects.ApplyQuery("$orderBy=IntValue, DoubleValue desc");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderBy_InnerObjectIntValueByAscThenDoubleValueByDesc_ReturnsOrdered()
        {
            var testObjects = new[]
            {
                new TestObject(){InnerObject = new InnerObject(){IntValue = 4}, DoubleValue = 1},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 3}, DoubleValue = 6},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 2}, DoubleValue = 4},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 2}, DoubleValue = 3},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 2}, DoubleValue = 5},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 2}, DoubleValue = 2},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 1}, DoubleValue = 7},
            }.AsQueryable();

            var expected = testObjects.OrderBy(x => x.InnerObject.IntValue).ThenByDescending(x => x.DoubleValue);
            var actual = testObjects.ApplyQuery("$orderBy=InnerObject.IntValue, DoubleValue desc");

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Filter

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void Filter_IntValueEqualsValue_ReturnsFilteredOne(int value)
        {
            var testObjects = new[]
            {
                new TestObject(){IntValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.IntValue == value);
            var actual = testObjects.ApplyQuery($"$filter=IntValue eq {value}");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(long.MaxValue)]
        [InlineData(long.MinValue)]
        public void Filter_LongValueEqualsValue_ReturnsFilteredOne(long value)
        {
            var testObjects = new[]
            {
                new TestObject(){LongValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.LongValue == value);
            var actual = testObjects.ApplyQuery($"$filter=LongValue eq {value}l");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.00)]
        [InlineData(-1.00)]
        public void Filter_DoubleValueEqualsValue_ReturnsFilteredOne(int value)
        {
            var testObjects = new[]
            {
                new TestObject(){DoubleValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.DoubleValue == value);
            var actual = testObjects.ApplyQuery($"$filter=DoubleValue eq {value}d");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Filter_BoolValueEqualsValue_ReturnsFilteredOne(bool value)
        {
            var testObjects = new[]
            {
                new TestObject(){BoolValue = value},
                new TestObject(){BoolValue = !value}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.BoolValue == value);
            var actual = testObjects.ApplyQuery($"$filter=BoolValue eq {value.ToString().ToLower()}");

            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(1.00)]
        [InlineData(-1.00)]
        public void Filter_DecimalValueEqualsValue_ReturnsFilteredOne(decimal value)
        {
            var testObjects = new[]
            {
                new TestObject(){DecimalValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.DecimalValue == value);
            var actual = testObjects.ApplyQuery($"$filter=DecimalValue eq {value}m");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.00)]
        [InlineData(-1.00)]
        public void Filter_FloatValueEqualsValue_ReturnsFilteredOne(float value)
        {
            var testObjects = new[]
            {
                new TestObject(){FloatValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.FloatValue == value);
            var actual = testObjects.ApplyQuery($"$filter=FloatValue eq {value}m");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2ec37a3c-1529-4298-a1da-30472b68e6a5")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void Filter_GuidValueEqualsValue_ReturnsFilteredOne(string value)
        {
            var testGuid = Guid.Parse(value);
            var testObjects = new[]
            {
                new TestObject(){GuidValue = testGuid},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.GuidValue == testGuid);
            var actual = testObjects.ApplyQuery($"$filter=GuidValue eq {testGuid}");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2000-01-01")]
        [InlineData("2000-01-01T12:59")]
        public void Filter_DateTimeValueEqualsValue_ReturnsFilteredOne(string value)
        {
            var testDateTime = DateTime.Parse(value);
            var testObjects = new[]
            {
                new TestObject(){DateTimeValue = testDateTime},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.DateTimeValue == testDateTime);
            var actual = testObjects.ApplyQuery($"$filter=DateTimeValue eq datetime'{value}'");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("notEmptyString")]
        [InlineData("")]
        public void Filter_StringValueEqualsValue_ReturnsFilteredOne(string value)
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue == value);
            var actual = testObjects.ApplyQuery($"$filter=StringValue eq '{value}'");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_StringValueEqualsNull_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(){StringValue = "notMatch"}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue == null);
            var actual = testObjects.ApplyQuery("$filter=StringValue eq null");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public void Filter_NullableIntValueEqualsValue_ReturnsFilteredOne(int? value)
        {
            var testObjects = new[]
            {
                new TestObject(){NullableIntValue = value},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.NullableIntValue == value);
            var actual = testObjects.ApplyQuery($"$filter=NullableIntValue eq {value}");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_NullableIntValueEqualsNull_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){NullableIntValue = null},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.NullableIntValue == null);
            var actual = testObjects.ApplyQuery("$filter=NullableIntValue eq null");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_StringValueStartsWithSomeValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = "match"},
                new TestObject(){StringValue = "notMatch"}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue.StartsWith("match"));
            var actual = testObjects.ApplyQuery("$filter=startswith(StringValue, 'match') eq true");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_StringValueEndsWithSomeValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = "match"},
                new TestObject(){StringValue = "notMatch"}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue.EndsWith("match"));
            var actual = testObjects.ApplyQuery("$filter=endswith(StringValue, 'match') eq true");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_ToUpperStringValueEqualsSomeValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = "match"},
                new TestObject(){StringValue = "notMatch"}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue.ToUpper() == "MATCH");
            var actual = testObjects.ApplyQuery("$filter=toupper(StringValue) eq 'MATCH'");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_ToLowerStringValueEqualsSomeValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){StringValue = "MATCH"},
                new TestObject(){StringValue = "notMatch"}
            }.AsQueryable();

            var expected = testObjects.Where(x => x.StringValue.ToLower() == "match");
            var actual = testObjects.ApplyQuery("$filter=tolower(StringValue) eq 'match'");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_StringValueNotEqualsSomeString_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(),
                new TestObject(){StringValue = "notMatch"},
            }.AsQueryable();

            var expected = testObjects.Where(x => !(x.StringValue == "notMatch"));
            var actual = testObjects.ApplyQuery("$filter=not StringValue eq 'notMatch'");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Filter_InnerObjectIntValueEqualsValue_ReturnsFilteredOne()
        {
            var testObjects = new[]
            {
                new TestObject(){InnerObject = new InnerObject(){IntValue = 1}},
                new TestObject()
            }.AsQueryable();

            var expected = testObjects.Where(x => x.InnerObject != null && x.InnerObject.IntValue == 1);
            var actual = testObjects.ApplyQuery("$filter=InnerObject ne null and InnerObject.IntValue eq 1");

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Select

        [Fact]
        public void Select_OneProperty_ReturnsOneWithOneProperty()
        {
            IQueryable testObjects = new[]
            {
                new TestObject(){IntValue = 1, DoubleValue = 1},
            }.AsQueryable();

            var actual = (IQueryable)testObjects.ApplyQuery("$select=IntValue");

            Assert.True(actual.ElementType != typeof(TestObject));
            Assert.True(actual.ElementType.GetFields().Length == 1);
            Assert.True(actual.ElementType.GetFields().First().Name == "IntValue");
        }

        #endregion
    }
}
