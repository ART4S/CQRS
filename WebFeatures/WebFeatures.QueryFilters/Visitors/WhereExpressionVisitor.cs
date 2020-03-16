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
    internal class WhereExpressionVisitor : QueryFiltersBaseVisitor<BaseNode>
    {
        private readonly ParameterExpression _parameter;

        public WhereExpressionVisitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        public override BaseNode VisitWhereExpression(QueryFiltersParser.WhereExpressionContext context)
        {
            BaseNode resultNode = context.children[0].Accept(this);

            for (int i = 1; i < context.children.Count; i += 2)
            {
                var left = resultNode;
                var right = context.children[i + 1].Accept(this);

                var aggregateNode = (ITerminalNode)context.children[i];

                switch (aggregateNode.Symbol.Type)
                {
                    case QueryFiltersLexer.AND:
                        resultNode = new AndNode(left, right);
                        continue;
                    case QueryFiltersLexer.OR:
                        resultNode = new OrNode(left, right);
                        continue;
                    default:
                        throw new ParseRuleException(
                            nameof(WhereExpressionVisitor),
                            $"Unknown predicate type: '{aggregateNode.Symbol.Type}'");
                }
            }

            return resultNode;
        }

        public override BaseNode VisitWhereAtom(QueryFiltersParser.WhereAtomContext context)
        {
            var resultNode = context.boolExpr?.Accept(this) ??
                             context.whereExpr?.Accept(this) ??
                             throw new ParseRuleException(nameof(WhereExpressionVisitor));

            if (context.not != null)
            {
                resultNode = new NotNode(resultNode);
            }

            return resultNode;
        }

        public override BaseNode VisitAtom(QueryFiltersParser.AtomContext context)
        {
            foreach (var child in context.children)
            {
                var node = child.Accept(this);
                if (node != null)
                {
                    return node;
                }
            }

            throw new ParseRuleException(nameof(WhereExpressionVisitor));
        }

        public override BaseNode VisitBoolExpression(QueryFiltersParser.BoolExpressionContext context)
        {
            var left = context.left.Accept(this);
            var right = context.right.Accept(this);

            switch (context.operation.Type)
            {
                case QueryFiltersLexer.EQUALS:
                    return new EqualsNode(left, right);
                case QueryFiltersLexer.NOTEQUALS:
                    return new NotEqualsNode(left, right);
                case QueryFiltersLexer.GREATERTHAN:
                    return new GreaterThanNode(left, right);
                case QueryFiltersLexer.GREATERTHANOREQUAL:
                    return new GreaterThanOrEqualNode(left, right);
                case QueryFiltersLexer.LESSTHAN:
                    return new LessThanNode(left, right);
                case QueryFiltersLexer.LESSTHANOREQUAL:
                    return new LessThanOrEqualNode(left, right);
                default:
                    throw new ParseRuleException(
                        nameof(WhereExpressionVisitor),
                        $"Unknown operation type: '{context.operation.Type}'");
            }
        }

        public override BaseNode VisitProperty(QueryFiltersParser.PropertyContext context)
        {
            return new PropertyNode(context.value.Text, _parameter);
        }

        public override BaseNode VisitConstant(QueryFiltersParser.ConstantContext context)
        {
            switch (context.value.Type)
            {
                case QueryFiltersLexer.INT:
                    return new IntNode(context.value.Text);
                case QueryFiltersLexer.LONG:
                    return new LongNode(context.value.Text);
                case QueryFiltersLexer.DOUBLE:
                    return new DoubleNode(context.value.Text);
                case QueryFiltersLexer.FLOAT:
                    return new FloatNode(context.value.Text);
                case QueryFiltersLexer.DECIMAL:
                    return new DecimalNode(context.value.Text);
                case QueryFiltersLexer.BOOL:
                    return new BoolNode(context.value.Text);
                case QueryFiltersLexer.GUID:
                    return new GuidNode(context.value.Text);
                case QueryFiltersLexer.NULL:
                    return new NullNode();
                case QueryFiltersLexer.STRING:
                    return new StringNode(context.value.Text);
                case QueryFiltersLexer.DATETIME:
                    return new DateTimeNode(context.value.Text);
                default:
                    throw new ParseRuleException(
                        nameof(WhereExpressionVisitor),
                        $"Unknown data type: '{context.value.Type}'");
            }
        }

        public override BaseNode VisitFunction(QueryFiltersParser.FunctionContext context)
        {
            var parameters = context.atom().Select(x => x.Accept(this)).ToArray();

            switch (context.value.Type)
            {
                case QueryFiltersParser.TOUPPER:
                    return new ToUpperNode(parameters);
                case QueryFiltersParser.TOLOWER:
                    return new ToLowerNode(parameters);
                case QueryFiltersParser.STARTSWITH:
                    return new StartsWithNode(parameters);
                case QueryFiltersParser.ENDSWITH:
                    return new EndsWithNode(parameters);
                default:
                    throw new ParseRuleException(
                        nameof(WhereExpressionVisitor),
                        $"Unknown function type: '{context.value.Type}'");
            }
        }
    }
}
