using System.Linq;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.Helpers;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.Functions
{
    internal class ToLowerNode : FunctionNode
    {
        public ToLowerNode(BaseNode[] parameters) : base(parameters)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Call(
                Parameters[0].CreateExpression(),
                ReflectionCache.ToLower,
                Parameters.Skip(1).Select(x => x.CreateExpression()));
        }
    }
}
