using System.Globalization;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.DataTypes
{
    internal class DoubleNode : SingleNode
    {
        public DoubleNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(double.Parse(Value.Replace("d",""), CultureInfo.InvariantCulture));
        }
    }
}
