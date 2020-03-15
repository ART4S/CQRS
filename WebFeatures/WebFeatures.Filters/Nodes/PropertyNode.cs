using System.Linq;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes
{
    internal class PropertyNode : SingleNode
    {
        private readonly ParameterExpression _parameter;

        public PropertyNode(string value, ParameterExpression parameter) : base(value)
        {
            _parameter = parameter;
        }

        public override Expression CreateExpression()
        {
            return Value.Split(".").Aggregate<string, MemberExpression>(null, 
                (current, propertyName) => current == null ? 
                    Expression.Property(_parameter, propertyName) : 
                    Expression.Property(current, propertyName));
        }
    }
}
