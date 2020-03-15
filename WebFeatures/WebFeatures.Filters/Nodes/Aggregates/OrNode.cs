using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.Aggregates
{
    internal class OrNode : AggregateNode
    {
        public OrNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.OrElse(Left.CreateExpression(), Right.CreateExpression());
        }
    }
}
