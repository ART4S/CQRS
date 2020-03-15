using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.Operators
{
    internal class GreaterThanOrEqualNode : OperationNode
    {
        public GreaterThanOrEqualNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        protected override Expression CreateExpressionImpl(Expression left, Expression right)
        {
            return Expression.GreaterThanOrEqual(left, right);
        }
    }
}
