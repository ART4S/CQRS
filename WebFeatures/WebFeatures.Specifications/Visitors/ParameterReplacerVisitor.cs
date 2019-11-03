using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebFeatures.Specifications.Visitors
{
    internal class ParameterReplacerVisitor : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _parametersMap;

        public ParameterReplacerVisitor(Dictionary<ParameterExpression, ParameterExpression> parametersMap)
        {
            _parametersMap = parametersMap;
        }

        protected override Expression VisitParameter(ParameterExpression param)
        {
            if (_parametersMap.TryGetValue(param, out var replacement))
            {
                param = replacement;
            }

            return base.VisitParameter(param);
        }
    }
}