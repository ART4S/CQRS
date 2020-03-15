using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.DataTypes
{
    internal class NullNode : BaseNode
    {
        public override Expression CreateExpression()
        {
            return Expression.Constant(null);
        }
    }
}
