using System.Linq.Expressions;

namespace WebFeatures.Specifications.Visitors
{
    internal class ExpressionReplacerVisitor : ExpressionVisitor
    {
        private readonly Expression _source;
        private readonly Expression _replacement;

        public ExpressionReplacerVisitor(Expression source, Expression replacement)
        {
            _source = source;
            _replacement = replacement;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _source)
            {
                node = _replacement;
            }

            return base.Visit(node);
        }
    }
}