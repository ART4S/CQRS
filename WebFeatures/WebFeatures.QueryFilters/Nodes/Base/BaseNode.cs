using System.Linq.Expressions;

namespace WebFeatures.QueryFilters.Nodes.Base
{
    internal abstract class BaseNode
    {
        public abstract Expression CreateExpression();
    }
}
