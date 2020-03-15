using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.DataTypes
{
    internal class IntNode : SingleNode
    {
        public IntNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(int.Parse(Value));
        }
    }
}
