using Antlr4.Runtime.Tree;
using System.Linq;
using System.Linq.Expressions;
using WebFeatures.QueryFilters.AntlrGenerated;
using WebFeatures.QueryFilters.Exceptions;
using WebFeatures.QueryFilters.Nodes;
using WebFeatures.QueryFilters.Nodes.Aggregates;
using WebFeatures.QueryFilters.Nodes.Base;
using WebFeatures.QueryFilters.Nodes.DataTypes;
using WebFeatures.QueryFilters.Nodes.Functions;
using WebFeatures.QueryFilters.Nodes.Operators;

namespace WebFeatures.QueryFilters.Visitors
{
    internal class FilterExpressionVisitor : QueryFilteringBaseVisitor<BaseNode>
    {
        private readonly ParameterExpression _parameter;

        public FilterExpressionVisitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        public override BaseNode VisitFilterExpression(QueryFilteringParser.FilterExpressionContext context)
        {
            BaseNode resultNode = context.children[0].Accept(this);

            for (int i = 1; i < context.children.Count; i += 2)
            {
                var left = resultNode;
                var right = context.children[i + 1].Accept(this);

                var aggregateNode = (ITerminalNode)context.children[i];

                switch (aggregateNode.Symbol.Type)
                {
                    case QueryFilteringLexer.AND:
                        resultNode = new AndNode(left, right);
                        continue;
                    case QueryFilteringLexer.OR:
                        resultNode = new OrNode(left, right);
                        continue;
                    default:
                        throw new ParseRuleException(
                            nameof(FilterExpressionVisitor),
                            $"Unknown predicate type: '{aggregateNode.Symbol.Type}'");
                }
            }

            return resultNode;
        }

        public override BaseNode VisitFilterAtom(QueryFilteringParser.FilterAtomContext context)
        {
            var resultNode = context.boolExpr?.Accept(this) ??
                             context.filterExpr?.Accept(this) ??
                             throw new ParseRuleException(nameof(FilterExpressionVisitor));

            if (context.not != null)
            {
                resultNode = new NotNode(resultNode);
            }

            return resultNode;
        }

        public override BaseNode VisitAtom(QueryFilteringParser.AtomContext context)
        {
            foreach (var child in context.children)
            {
                var node = child.Accept(this);
                if (node != null)
                {
                    return node;
                }
            }

            throw new ParseRuleException(nameof(FilterExpressionVisitor));
        }

        public override BaseNode VisitBoolExpression(QueryFilteringParser.BoolExpressionContext context)
        {
            var left = context.left.Accept(this);
            var right = context.right.Accept(this);

            switch (context.operation.Type)
            {
                case QueryFilteringLexer.EQUALS:
                    return new EqualsNode(left, right);
                case QueryFilteringLexer.NOTEQUALS:
                    return new NotEqualsNode(left, right);
                case QueryFilteringLexer.GREATERTHAN:
                    return new GreaterThanNode(left, right);
                case QueryFilteringLexer.GREATERTHANOREQUAL:
                    return new GreaterThanOrEqualNode(left, right);
                case QueryFilteringLexer.LESSTHAN:
                    return new LessThanNode(left, right);
                case QueryFilteringLexer.LESSTHANOREQUAL:
                    return new LessThanOrEqualNode(left, right);
                default:
                    throw new ParseRuleException(
                        nameof(FilterExpressionVisitor),
                        $"Unknown operation type: '{context.operation.Type}'");
            }
        }

        public override BaseNode VisitProperty(QueryFilteringParser.PropertyContext context)
        {
            return new PropertyNode(context.value.Text, _parameter);
        }

        public override BaseNode VisitConstant(QueryFilteringParser.ConstantContext context)
        {
            switch (context.value.Type)
            {
                case QueryFilteringLexer.INT:
                    return new IntNode(context.value.Text);
                case QueryFilteringLexer.LONG:
                    return new LongNode(context.value.Text);
                case QueryFilteringLexer.DOUBLE:
                    return new DoubleNode(context.value.Text);
                case QueryFilteringLexer.FLOAT:
                    return new FloatNode(context.value.Text);
                case QueryFilteringLexer.DECIMAL:
                    return new DecimalNode(context.value.Text);
                case QueryFilteringLexer.BOOL:
                    return new BoolNode(context.value.Text);
                case QueryFilteringLexer.GUID:
                    return new GuidNode(context.value.Text);
                case QueryFilteringLexer.NULL:
                    return new NullNode();
                case QueryFilteringLexer.STRING:
                    return new StringNode(context.value.Text);
                case QueryFilteringLexer.DATETIME:
                    return new DateTimeNode(context.value.Text);
                default:
                    throw new ParseRuleException(
                        nameof(FilterExpressionVisitor),
                        $"Unknown data type: '{context.value.Type}'");
            }
        }

        public override BaseNode VisitFunction(QueryFilteringParser.FunctionContext context)
        {
            var parameters = context.atom().Select(x => x.Accept(this)).ToArray();

            switch (context.value.Type)
            {
                case QueryFilteringParser.TOUPPER:
                    return new ToUpperNode(parameters);
                case QueryFilteringParser.TOLOWER:
                    return new ToLowerNode(parameters);
                case QueryFilteringParser.STARTSWITH:
                    return new StartsWithNode(parameters);
                case QueryFilteringParser.ENDSWITH:
                    return new EndsWithNode(parameters);
                default:
                    throw new ParseRuleException(
                        nameof(FilterExpressionVisitor),
                        $"Unknown function type: '{context.value.Type}'");
            }
        }
    }
}
