using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace WebFeatures.QueryFilters.AntlrGenerated
{
    /// <summary>
    /// This class provides an empty implementation of <see cref="IQueryFiltersVisitor{Result}"/>,
    /// which can be extended to create a visitor which only needs to handle a subset
    /// of the available methods.
    /// </summary>
    /// <typeparam name="TResult">The return type of the visit operation.</typeparam>
    internal class QueryFiltersBaseVisitor<TResult> : AbstractParseTreeVisitor<TResult>, IQueryFiltersVisitor<TResult>
    {
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.query"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitQuery([NotNull] QueryFiltersParser.QueryContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.queryFunction"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitQueryFunction([NotNull] QueryFiltersParser.QueryFunctionContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.top"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitTop([NotNull] QueryFiltersParser.TopContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.skip"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitSkip([NotNull] QueryFiltersParser.SkipContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.orderBy"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitOrderBy([NotNull] QueryFiltersParser.OrderByContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.orderByExpression"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitOrderByExpression([NotNull] QueryFiltersParser.OrderByExpressionContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.orderByAtom"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitOrderByAtom([NotNull] QueryFiltersParser.OrderByAtomContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.select"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitSelect([NotNull] QueryFiltersParser.SelectContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.selectExpression"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitSelectExpression([NotNull] QueryFiltersParser.SelectExpressionContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.where"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitWhere([NotNull] QueryFiltersParser.WhereContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.whereExpression"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitWhereExpression([NotNull] QueryFiltersParser.WhereExpressionContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.whereAtom"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitWhereAtom([NotNull] QueryFiltersParser.WhereAtomContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.boolExpression"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitBoolExpression([NotNull] QueryFiltersParser.BoolExpressionContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.atom"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitAtom([NotNull] QueryFiltersParser.AtomContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.property"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitProperty([NotNull] QueryFiltersParser.PropertyContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.constant"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitConstant([NotNull] QueryFiltersParser.ConstantContext context) { return VisitChildren(context); }
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.function"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public virtual TResult VisitFunction([NotNull] QueryFiltersParser.FunctionContext context) { return VisitChildren(context); }
    }
}
