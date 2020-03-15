using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.Operators
{
    internal class NotNode : BaseNode
    {
        private readonly BaseNode _node;

        public NotNode(BaseNode node)
        {
            _node = node;
        }

        public override Expression CreateExpression()
        {
            var expression = _node.CreateExpression();
            if (expression.Type != typeof(bool))
            {
                expression = Expression.Convert(expression, typeof(bool));
            }

            return Expression.Not(expression);
        }
    }
}
