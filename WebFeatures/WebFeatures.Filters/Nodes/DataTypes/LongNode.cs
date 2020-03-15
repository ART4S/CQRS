using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.DataTypes
{
    internal class LongNode : SingleNode
    {
        public LongNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(long.Parse(Value.Replace("l","")));
        }
    }
}
