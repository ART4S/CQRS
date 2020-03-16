using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace WebFeatures.QueryFilters.AntlrGenerated
{
    /// <summary>
    /// This interface defines a complete generic visitor for a parse tree produced
    /// by <see cref="QueryFiltersParser"/>.
    /// </summary>
    /// <typeparam name="TResult">The return type of the visit operation.</typeparam>
    internal interface IQueryFiltersVisitor<out TResult> : IParseTreeVisitor<TResult>
    {
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.query"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitQuery([NotNull] QueryFiltersParser.QueryContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.queryFunction"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitQueryFunction([NotNull] QueryFiltersParser.QueryFunctionContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.top"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitTop([NotNull] QueryFiltersParser.TopContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.skip"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitSkip([NotNull] QueryFiltersParser.SkipContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.orderBy"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitOrderBy([NotNull] QueryFiltersParser.OrderByContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.orderByExpression"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitOrderByExpression([NotNull] QueryFiltersParser.OrderByExpressionContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.orderByAtom"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitOrderByAtom([NotNull] QueryFiltersParser.OrderByAtomContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.select"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitSelect([NotNull] QueryFiltersParser.SelectContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.selectExpression"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitSelectExpression([NotNull] QueryFiltersParser.SelectExpressionContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.where"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitWhere([NotNull] QueryFiltersParser.WhereContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.whereExpression"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitWhereExpression([NotNull] QueryFiltersParser.WhereExpressionContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.whereAtom"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitWhereAtom([NotNull] QueryFiltersParser.WhereAtomContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.boolExpression"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitBoolExpression([NotNull] QueryFiltersParser.BoolExpressionContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.atom"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitAtom([NotNull] QueryFiltersParser.AtomContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.property"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitProperty([NotNull] QueryFiltersParser.PropertyContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.constant"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitConstant([NotNull] QueryFiltersParser.ConstantContext context);
        /// <summary>
        /// Visit a parse tree produced by <see cref="QueryFiltersParser.function"/>.
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        TResult VisitFunction([NotNull] QueryFiltersParser.FunctionContext context);
    }
}
