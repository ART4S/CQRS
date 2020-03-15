using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.Operators
{
    internal class LessThanNode : OperationNode
    {
        public LessThanNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        protected override Expression CreateExpressionImpl(Expression left, Expression right)
        {
            return Expression.LessThan(left, right);
        }
    }
}
