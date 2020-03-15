using System.Linq.Expressions;

namespace WebFeatures.QueryFilters.Nodes.Base
{
    internal abstract class OperationNode : AggregateNode
    {
        protected OperationNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression CreateExpression()
        {
            var left = Left.CreateExpression();
            var right = Right.CreateExpression();

            if (left.Type != right.Type)
            {
                right = Expression.Convert(right, left.Type);
            }

            return CreateExpressionImpl(left, right);
        }

        protected abstract Expression CreateExpressionImpl(Expression left, Expression right);
    }
}
