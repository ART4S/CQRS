using System.Linq.Expressions;

namespace WebFeatures.Infrastructure.DataAccess.Mappings.Helpers
{
    internal class PropertyNameVisitor : ExpressionVisitor
    {
        public string PropertyName { get; private set; }

        public override Expression Visit(Expression node)
        {
            if (node is MemberExpression member)
            {
                // TODO:
            }

            return base.Visit(node);
        }
    }
}
