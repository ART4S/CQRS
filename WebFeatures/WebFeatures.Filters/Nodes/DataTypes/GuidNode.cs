using System;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.DataTypes
{
    internal class GuidNode : SingleNode
    {
        public GuidNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(Guid.Parse(Value));
        }
    }
}
