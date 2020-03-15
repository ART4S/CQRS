using System.Linq;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.Infrastructure;
using WebFeatures.QueryFilters.Nodes.Base;

namespace WebFeatures.QueryFilters.Nodes.Functions
{
    internal class EndsWithNode : FunctionNode
    {
        public EndsWithNode(BaseNode[] parameters) : base(parameters)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Call(
                Parameters[0].CreateExpression(),
                ReflectionCache.EndsWith,
                Parameters.Skip(1).Select(x => x.CreateExpression()));
        }
    }
}
