using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebFeatures.Specifications.Visitors
{
    internal class ParamsReplacerVisitor : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _replacementsMap;

        public ParamsReplacerVisitor(Dictionary<ParameterExpression, ParameterExpression> replacementsMap)
        {
            _replacementsMap = replacementsMap;
        }

        protected override Expression VisitParameter(ParameterExpression param)
        {
            if (_replacementsMap.TryGetValue(param, out var replacement))
            {
                param = replacement;
            }

            return base.VisitParameter(param);
        }
    }
}