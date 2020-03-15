using System.Globalization;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.DataTypes
{
    internal class FloatNode : SingleNode
    {
        public FloatNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(float.Parse(Value.Replace("m",""), CultureInfo.InvariantCulture));
        }
    }
}
