using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.DataTypes
{
    internal class StringNode : SingleNode
    {
        public StringNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(Value.Trim('\''));
        }
    }
}
