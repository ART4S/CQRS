using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.IO;
using DFA = Antlr4.Runtime.Dfa.DFA;

namespace WebFeatures.QueryFilters.AntlrGenerated
{
    internal class QueryFiltersParser : Parser
    {
        protected static DFA[] decisionToDFA;
        protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
        public const int
            T__0 = 1, T__1 = 2, T__2 = 3, T__3 = 4, T__4 = 5, T__5 = 6, T__6 = 7, T__7 = 8, T__8 = 9,
            T__9 = 10, OR = 11, AND = 12, NOT = 13, EQUALS = 14, NOTEQUALS = 15, GREATERTHAN = 16,
            GREATERTHANOREQUAL = 17, LESSTHAN = 18, LESSTHANOREQUAL = 19, ASC = 20, DESC = 21,
            TOUPPER = 22, TOLOWER = 23, STARTSWITH = 24, ENDSWITH = 25, INT = 26, LONG = 27, DOUBLE = 28,
            FLOAT = 29, DECIMAL = 30, BOOL = 31, GUID = 32, NULL = 33, DATETIME = 34, STRING = 35,
            PROPERTYACCESS = 36, WHITESPACE = 37;
        public const int
            RULE_query = 0, RULE_queryFunction = 1, RULE_top = 2, RULE_skip = 3, RULE_orderBy = 4,
            RULE_orderByExpression = 5, RULE_orderByAtom = 6, RULE_select = 7, RULE_selectExpression = 8,
            RULE_where = 9, RULE_whereExpression = 10, RULE_whereAtom = 11, RULE_boolExpression = 12,
            RULE_atom = 13, RULE_property = 14, RULE_constant = 15, RULE_function = 16;
        public static readonly string[] ruleNames = {
            "query", "queryFunction", "top", "skip", "orderBy", "orderByExpression",
            "orderByAtom", "select", "selectExpression", "where", "whereExpression",
            "whereAtom", "boolExpression", "atom", "property", "constant", "function"
        };

        private static readonly string[] _LiteralNames = {
            null, "'?'", "'&'", "'$top='", "'$skip='", "'$orderBy='", "','", "'$select='",
            "'$where='", "'('", "')'", "'or'", "'and'", "'not'", "'eq'", "'ne'", "'gt'",
            "'ge'", "'lt'", "'le'", "'asc'", "'desc'", "'toupper'", "'tolower'", "'startswith'",
            "'endswith'", null, null, null, null, null, null, null, "'null'"
        };
        private static readonly string[] _SymbolicNames = {
            null, null, null, null, null, null, null, null, null, null, null, "OR",
            "AND", "NOT", "EQUALS", "NOTEQUALS", "GREATERTHAN", "GREATERTHANOREQUAL",
            "LESSTHAN", "LESSTHANOREQUAL", "ASC", "DESC", "TOUPPER", "TOLOWER", "STARTSWITH",
            "ENDSWITH", "INT", "LONG", "DOUBLE", "FLOAT", "DECIMAL", "BOOL", "GUID",
            "NULL", "DATETIME", "STRING", "PROPERTYACCESS", "WHITESPACE"
        };
        public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

        [NotNull]
        public override IVocabulary Vocabulary
        {
            get
            {
                return DefaultVocabulary;
            }
        }

        public override string GrammarFileName { get { return "QueryFilters.g4"; } }

        public override string[] RuleNames { get { return ruleNames; } }

        public override string SerializedAtn { get { return new string(_serializedATN); } }

        static QueryFiltersParser()
        {
            decisionToDFA = new DFA[_ATN.NumberOfDecisions];
            for (int i = 0; i < _ATN.NumberOfDecisions; i++)
            {
                decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
            }
        }

        public QueryFiltersParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

        public QueryFiltersParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
            : base(input, output, errorOutput)
        {
            Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
        }

        public partial class QueryContext : ParserRuleContext
        {
            public QueryFunctionContext[] queryFunction()
            {
                return GetRuleContexts<QueryFunctionContext>();
            }
            public QueryFunctionContext queryFunction(int i)
            {
                return GetRuleContext<QueryFunctionContext>(i);
            }
            public QueryContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_query; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitQuery(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public QueryContext query()
        {
            QueryContext _localctx = new QueryContext(Context, State);
            EnterRule(_localctx, 0, RULE_query);
            int _la;
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 45;
                    ErrorHandler.Sync(this);
                    _la = TokenStream.LA(1);
                    if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__2) | (1L << T__3) | (1L << T__4) | (1L << T__6) | (1L << T__7))) != 0))
                    {
                        {
                            State = 35;
                            ErrorHandler.Sync(this);
                            _la = TokenStream.LA(1);
                            if (_la == T__0)
                            {
                                {
                                    State = 34; Match(T__0);
                                }
                            }

                            State = 37; queryFunction();
                            State = 42;
                            ErrorHandler.Sync(this);
                            _la = TokenStream.LA(1);
                            while (_la == T__1)
                            {
                                {
                                    {
                                        State = 38; Match(T__1);
                                        State = 39; queryFunction();
                                    }
                                }
                                State = 44;
                                ErrorHandler.Sync(this);
                                _la = TokenStream.LA(1);
                            }
                        }
                    }

                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class QueryFunctionContext : ParserRuleContext
        {
            public TopContext top()
            {
                return GetRuleContext<TopContext>(0);
            }
            public SkipContext skip()
            {
                return GetRuleContext<SkipContext>(0);
            }
            public OrderByContext orderBy()
            {
                return GetRuleContext<OrderByContext>(0);
            }
            public SelectContext select()
            {
                return GetRuleContext<SelectContext>(0);
            }
            public WhereContext where()
            {
                return GetRuleContext<WhereContext>(0);
            }
            public QueryFunctionContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_queryFunction; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitQueryFunction(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public QueryFunctionContext queryFunction()
        {
            QueryFunctionContext _localctx = new QueryFunctionContext(Context, State);
            EnterRule(_localctx, 2, RULE_queryFunction);
            try
            {
                State = 52;
                ErrorHandler.Sync(this);
                switch (TokenStream.LA(1))
                {
                    case T__2:
                        EnterOuterAlt(_localctx, 1);
                        {
                            State = 47; top();
                        }
                        break;
                    case T__3:
                        EnterOuterAlt(_localctx, 2);
                        {
                            State = 48; skip();
                        }
                        break;
                    case T__4:
                        EnterOuterAlt(_localctx, 3);
                        {
                            State = 49; orderBy();
                        }
                        break;
                    case T__6:
                        EnterOuterAlt(_localctx, 4);
                        {
                            State = 50; select();
                        }
                        break;
                    case T__7:
                        EnterOuterAlt(_localctx, 5);
                        {
                            State = 51; where();
                        }
                        break;
                    default:
                        throw new NoViableAltException(this);
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class TopContext : ParserRuleContext
        {
            public IToken count;
            public ITerminalNode INT() { return GetToken(QueryFiltersParser.INT, 0); }
            public TopContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_top; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitTop(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public TopContext top()
        {
            TopContext _localctx = new TopContext(Context, State);
            EnterRule(_localctx, 4, RULE_top);
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 54; Match(T__2);
                    State = 55; _localctx.count = Match(INT);
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class SkipContext : ParserRuleContext
        {
            public IToken count;
            public ITerminalNode INT() { return GetToken(QueryFiltersParser.INT, 0); }
            public SkipContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_skip; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitSkip(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public SkipContext skip()
        {
            SkipContext _localctx = new SkipContext(Context, State);
            EnterRule(_localctx, 6, RULE_skip);
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 57; Match(T__3);
                    State = 58; _localctx.count = Match(INT);
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class OrderByContext : ParserRuleContext
        {
            public OrderByExpressionContext expression;
            public OrderByExpressionContext orderByExpression()
            {
                return GetRuleContext<OrderByExpressionContext>(0);
            }
            public OrderByContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_orderBy; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitOrderBy(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public OrderByContext orderBy()
        {
            OrderByContext _localctx = new OrderByContext(Context, State);
            EnterRule(_localctx, 8, RULE_orderBy);
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 60; Match(T__4);
                    State = 61; _localctx.expression = orderByExpression();
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class OrderByExpressionContext : ParserRuleContext
        {
            public OrderByAtomContext[] orderByAtom()
            {
                return GetRuleContexts<OrderByAtomContext>();
            }
            public OrderByAtomContext orderByAtom(int i)
            {
                return GetRuleContext<OrderByAtomContext>(i);
            }
            public OrderByExpressionContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_orderByExpression; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitOrderByExpression(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public OrderByExpressionContext orderByExpression()
        {
            OrderByExpressionContext _localctx = new OrderByExpressionContext(Context, State);
            EnterRule(_localctx, 10, RULE_orderByExpression);
            int _la;
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 63; orderByAtom(true);
                    State = 68;
                    ErrorHandler.Sync(this);
                    _la = TokenStream.LA(1);
                    while (_la == T__5)
                    {
                        {
                            {
                                State = 64; Match(T__5);
                                State = 65; orderByAtom(false);
                            }
                        }
                        State = 70;
                        ErrorHandler.Sync(this);
                        _la = TokenStream.LA(1);
                    }
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class OrderByAtomContext : ParserRuleContext
        {
            public bool isFirstSort;
            public IToken propertyName;
            public IToken sortType;
            public ITerminalNode PROPERTYACCESS() { return GetToken(QueryFiltersParser.PROPERTYACCESS, 0); }
            public ITerminalNode ASC() { return GetToken(QueryFiltersParser.ASC, 0); }
            public ITerminalNode DESC() { return GetToken(QueryFiltersParser.DESC, 0); }
            public OrderByAtomContext(ParserRuleContext parent, int invokingState) : base(parent, invokingState) { }
            public OrderByAtomContext(ParserRuleContext parent, int invokingState, bool isFirstSort)
                : base(parent, invokingState)
            {
                this.isFirstSort = isFirstSort;
            }
            public override int RuleIndex { get { return RULE_orderByAtom; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitOrderByAtom(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public OrderByAtomContext orderByAtom(bool isFirstSort)
        {
            OrderByAtomContext _localctx = new OrderByAtomContext(Context, State, isFirstSort);
            EnterRule(_localctx, 12, RULE_orderByAtom);
            int _la;
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 71; _localctx.propertyName = Match(PROPERTYACCESS);
                    State = 73;
                    ErrorHandler.Sync(this);
                    _la = TokenStream.LA(1);
                    if (_la == ASC || _la == DESC)
                    {
                        {
                            State = 72;
                            _localctx.sortType = TokenStream.LT(1);
                            _la = TokenStream.LA(1);
                            if (!(_la == ASC || _la == DESC))
                            {
                                _localctx.sortType = ErrorHandler.RecoverInline(this);
                            }
                            else
                            {
                                ErrorHandler.ReportMatch(this);
                                Consume();
                            }
                        }
                    }

                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class SelectContext : ParserRuleContext
        {
            public SelectExpressionContext expression;
            public SelectExpressionContext selectExpression()
            {
                return GetRuleContext<SelectExpressionContext>(0);
            }
            public SelectContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_select; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitSelect(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public SelectContext select()
        {
            SelectContext _localctx = new SelectContext(Context, State);
            EnterRule(_localctx, 14, RULE_select);
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 75; Match(T__6);
                    State = 76; _localctx.expression = selectExpression();
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class SelectExpressionContext : ParserRuleContext
        {
            public ITerminalNode[] PROPERTYACCESS() { return GetTokens(QueryFiltersParser.PROPERTYACCESS); }
            public ITerminalNode PROPERTYACCESS(int i)
            {
                return GetToken(QueryFiltersParser.PROPERTYACCESS, i);
            }
            public SelectExpressionContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_selectExpression; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitSelectExpression(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public SelectExpressionContext selectExpression()
        {
            SelectExpressionContext _localctx = new SelectExpressionContext(Context, State);
            EnterRule(_localctx, 16, RULE_selectExpression);
            int _la;
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 78; Match(PROPERTYACCESS);
                    State = 83;
                    ErrorHandler.Sync(this);
                    _la = TokenStream.LA(1);
                    while (_la == T__5)
                    {
                        {
                            {
                                State = 79; Match(T__5);
                                State = 80; Match(PROPERTYACCESS);
                            }
                        }
                        State = 85;
                        ErrorHandler.Sync(this);
                        _la = TokenStream.LA(1);
                    }
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class WhereContext : ParserRuleContext
        {
            public WhereExpressionContext expression;
            public WhereExpressionContext whereExpression()
            {
                return GetRuleContext<WhereExpressionContext>(0);
            }
            public WhereContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_where; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitWhere(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public WhereContext where()
        {
            WhereContext _localctx = new WhereContext(Context, State);
            EnterRule(_localctx, 18, RULE_where);
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 86; Match(T__7);
                    State = 87; _localctx.expression = whereExpression();
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class WhereExpressionContext : ParserRuleContext
        {
            public WhereAtomContext[] whereAtom()
            {
                return GetRuleContexts<WhereAtomContext>();
            }
            public WhereAtomContext whereAtom(int i)
            {
                return GetRuleContext<WhereAtomContext>(i);
            }
            public ITerminalNode[] OR() { return GetTokens(QueryFiltersParser.OR); }
            public ITerminalNode OR(int i)
            {
                return GetToken(QueryFiltersParser.OR, i);
            }
            public ITerminalNode[] AND() { return GetTokens(QueryFiltersParser.AND); }
            public ITerminalNode AND(int i)
            {
                return GetToken(QueryFiltersParser.AND, i);
            }
            public WhereExpressionContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_whereExpression; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitWhereExpression(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public WhereExpressionContext whereExpression()
        {
            WhereExpressionContext _localctx = new WhereExpressionContext(Context, State);
            EnterRule(_localctx, 20, RULE_whereExpression);
            int _la;
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 89; whereAtom();
                    State = 94;
                    ErrorHandler.Sync(this);
                    _la = TokenStream.LA(1);
                    while (_la == OR || _la == AND)
                    {
                        {
                            {
                                State = 90;
                                _la = TokenStream.LA(1);
                                if (!(_la == OR || _la == AND))
                                {
                                    ErrorHandler.RecoverInline(this);
                                }
                                else
                                {
                                    ErrorHandler.ReportMatch(this);
                                    Consume();
                                }
                                State = 91; whereAtom();
                            }
                        }
                        State = 96;
                        ErrorHandler.Sync(this);
                        _la = TokenStream.LA(1);
                    }
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class WhereAtomContext : ParserRuleContext
        {
            public IToken not;
            public BoolExpressionContext boolExpr;
            public WhereExpressionContext whereExpr;
            public BoolExpressionContext boolExpression()
            {
                return GetRuleContext<BoolExpressionContext>(0);
            }
            public WhereExpressionContext whereExpression()
            {
                return GetRuleContext<WhereExpressionContext>(0);
            }
            public ITerminalNode NOT() { return GetToken(QueryFiltersParser.NOT, 0); }
            public WhereAtomContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_whereAtom; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitWhereAtom(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public WhereAtomContext whereAtom()
        {
            WhereAtomContext _localctx = new WhereAtomContext(Context, State);
            EnterRule(_localctx, 22, RULE_whereAtom);
            int _la;
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 98;
                    ErrorHandler.Sync(this);
                    _la = TokenStream.LA(1);
                    if (_la == NOT)
                    {
                        {
                            State = 97; _localctx.not = Match(NOT);
                        }
                    }

                    State = 105;
                    ErrorHandler.Sync(this);
                    switch (TokenStream.LA(1))
                    {
                        case TOUPPER:
                        case TOLOWER:
                        case STARTSWITH:
                        case ENDSWITH:
                        case INT:
                        case LONG:
                        case DOUBLE:
                        case FLOAT:
                        case DECIMAL:
                        case BOOL:
                        case GUID:
                        case NULL:
                        case DATETIME:
                        case STRING:
                        case PROPERTYACCESS:
                            {
                                State = 100; _localctx.boolExpr = boolExpression();
                            }
                            break;
                        case T__8:
                            {
                                State = 101; Match(T__8);
                                State = 102; _localctx.whereExpr = whereExpression();
                                State = 103; Match(T__9);
                            }
                            break;
                        default:
                            throw new NoViableAltException(this);
                    }
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class BoolExpressionContext : ParserRuleContext
        {
            public AtomContext left;
            public IToken operation;
            public AtomContext right;
            public AtomContext[] atom()
            {
                return GetRuleContexts<AtomContext>();
            }
            public AtomContext atom(int i)
            {
                return GetRuleContext<AtomContext>(i);
            }
            public ITerminalNode EQUALS() { return GetToken(QueryFiltersParser.EQUALS, 0); }
            public ITerminalNode NOTEQUALS() { return GetToken(QueryFiltersParser.NOTEQUALS, 0); }
            public ITerminalNode GREATERTHAN() { return GetToken(QueryFiltersParser.GREATERTHAN, 0); }
            public ITerminalNode GREATERTHANOREQUAL() { return GetToken(QueryFiltersParser.GREATERTHANOREQUAL, 0); }
            public ITerminalNode LESSTHAN() { return GetToken(QueryFiltersParser.LESSTHAN, 0); }
            public ITerminalNode LESSTHANOREQUAL() { return GetToken(QueryFiltersParser.LESSTHANOREQUAL, 0); }
            public BoolExpressionContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_boolExpression; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitBoolExpression(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public BoolExpressionContext boolExpression()
        {
            BoolExpressionContext _localctx = new BoolExpressionContext(Context, State);
            EnterRule(_localctx, 24, RULE_boolExpression);
            int _la;
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 107; _localctx.left = atom();
                    State = 108;
                    _localctx.operation = TokenStream.LT(1);
                    _la = TokenStream.LA(1);
                    if (!((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << EQUALS) | (1L << NOTEQUALS) | (1L << GREATERTHAN) | (1L << GREATERTHANOREQUAL) | (1L << LESSTHAN) | (1L << LESSTHANOREQUAL))) != 0)))
                    {
                        _localctx.operation = ErrorHandler.RecoverInline(this);
                    }
                    else
                    {
                        ErrorHandler.ReportMatch(this);
                        Consume();
                    }
                    State = 109; _localctx.right = atom();
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class AtomContext : ParserRuleContext
        {
            public PropertyContext property()
            {
                return GetRuleContext<PropertyContext>(0);
            }
            public ConstantContext constant()
            {
                return GetRuleContext<ConstantContext>(0);
            }
            public FunctionContext function()
            {
                return GetRuleContext<FunctionContext>(0);
            }
            public AtomContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_atom; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitAtom(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public AtomContext atom()
        {
            AtomContext _localctx = new AtomContext(Context, State);
            EnterRule(_localctx, 26, RULE_atom);
            try
            {
                State = 114;
                ErrorHandler.Sync(this);
                switch (TokenStream.LA(1))
                {
                    case PROPERTYACCESS:
                        EnterOuterAlt(_localctx, 1);
                        {
                            State = 111; property();
                        }
                        break;
                    case INT:
                    case LONG:
                    case DOUBLE:
                    case FLOAT:
                    case DECIMAL:
                    case BOOL:
                    case GUID:
                    case NULL:
                    case DATETIME:
                    case STRING:
                        EnterOuterAlt(_localctx, 2);
                        {
                            State = 112; constant();
                        }
                        break;
                    case TOUPPER:
                    case TOLOWER:
                    case STARTSWITH:
                    case ENDSWITH:
                        EnterOuterAlt(_localctx, 3);
                        {
                            State = 113; function();
                        }
                        break;
                    default:
                        throw new NoViableAltException(this);
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class PropertyContext : ParserRuleContext
        {
            public IToken value;
            public ITerminalNode PROPERTYACCESS() { return GetToken(QueryFiltersParser.PROPERTYACCESS, 0); }
            public PropertyContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_property; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitProperty(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public PropertyContext property()
        {
            PropertyContext _localctx = new PropertyContext(Context, State);
            EnterRule(_localctx, 28, RULE_property);
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 116; _localctx.value = Match(PROPERTYACCESS);
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class ConstantContext : ParserRuleContext
        {
            public IToken value;
            public ITerminalNode INT() { return GetToken(QueryFiltersParser.INT, 0); }
            public ITerminalNode LONG() { return GetToken(QueryFiltersParser.LONG, 0); }
            public ITerminalNode DOUBLE() { return GetToken(QueryFiltersParser.DOUBLE, 0); }
            public ITerminalNode FLOAT() { return GetToken(QueryFiltersParser.FLOAT, 0); }
            public ITerminalNode DECIMAL() { return GetToken(QueryFiltersParser.DECIMAL, 0); }
            public ITerminalNode BOOL() { return GetToken(QueryFiltersParser.BOOL, 0); }
            public ITerminalNode NULL() { return GetToken(QueryFiltersParser.NULL, 0); }
            public ITerminalNode GUID() { return GetToken(QueryFiltersParser.GUID, 0); }
            public ITerminalNode STRING() { return GetToken(QueryFiltersParser.STRING, 0); }
            public ITerminalNode DATETIME() { return GetToken(QueryFiltersParser.DATETIME, 0); }
            public ConstantContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_constant; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitConstant(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public ConstantContext constant()
        {
            ConstantContext _localctx = new ConstantContext(Context, State);
            EnterRule(_localctx, 30, RULE_constant);
            int _la;
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 118;
                    _localctx.value = TokenStream.LT(1);
                    _la = TokenStream.LA(1);
                    if (!((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << INT) | (1L << LONG) | (1L << DOUBLE) | (1L << FLOAT) | (1L << DECIMAL) | (1L << BOOL) | (1L << GUID) | (1L << NULL) | (1L << DATETIME) | (1L << STRING))) != 0)))
                    {
                        _localctx.value = ErrorHandler.RecoverInline(this);
                    }
                    else
                    {
                        ErrorHandler.ReportMatch(this);
                        Consume();
                    }
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        public partial class FunctionContext : ParserRuleContext
        {
            public IToken value;
            public AtomContext[] atom()
            {
                return GetRuleContexts<AtomContext>();
            }
            public AtomContext atom(int i)
            {
                return GetRuleContext<AtomContext>(i);
            }
            public ITerminalNode TOUPPER() { return GetToken(QueryFiltersParser.TOUPPER, 0); }
            public ITerminalNode TOLOWER() { return GetToken(QueryFiltersParser.TOLOWER, 0); }
            public ITerminalNode STARTSWITH() { return GetToken(QueryFiltersParser.STARTSWITH, 0); }
            public ITerminalNode ENDSWITH() { return GetToken(QueryFiltersParser.ENDSWITH, 0); }
            public FunctionContext(ParserRuleContext parent, int invokingState)
                : base(parent, invokingState)
            {
            }
            public override int RuleIndex { get { return RULE_function; } }
            public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor)
            {
                IQueryFiltersVisitor<TResult> typedVisitor = visitor as IQueryFiltersVisitor<TResult>;
                if (typedVisitor != null) return typedVisitor.VisitFunction(this);
                else return visitor.VisitChildren(this);
            }
        }

        [RuleVersion(0)]
        public FunctionContext function()
        {
            FunctionContext _localctx = new FunctionContext(Context, State);
            EnterRule(_localctx, 32, RULE_function);
            int _la;
            try
            {
                EnterOuterAlt(_localctx, 1);
                {
                    State = 120;
                    _localctx.value = TokenStream.LT(1);
                    _la = TokenStream.LA(1);
                    if (!((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << TOUPPER) | (1L << TOLOWER) | (1L << STARTSWITH) | (1L << ENDSWITH))) != 0)))
                    {
                        _localctx.value = ErrorHandler.RecoverInline(this);
                    }
                    else
                    {
                        ErrorHandler.ReportMatch(this);
                        Consume();
                    }
                    State = 121; Match(T__8);
                    State = 122; atom();
                    State = 127;
                    ErrorHandler.Sync(this);
                    _la = TokenStream.LA(1);
                    while (_la == T__5)
                    {
                        {
                            {
                                State = 123; Match(T__5);
                                State = 124; atom();
                            }
                        }
                        State = 129;
                        ErrorHandler.Sync(this);
                        _la = TokenStream.LA(1);
                    }
                    State = 130; Match(T__9);
                }
            }
            catch (RecognitionException re)
            {
                _localctx.exception = re;
                ErrorHandler.ReportError(this, re);
                ErrorHandler.Recover(this, re);
            }
            finally
            {
                ExitRule();
            }
            return _localctx;
        }

        private static char[] _serializedATN = {
            '\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786',
            '\x5964', '\x3', '\'', '\x87', '\x4', '\x2', '\t', '\x2', '\x4', '\x3',
            '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', '\x5', '\x4',
            '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', '\t', '\b',
            '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', '\t', '\v',
            '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', '\t',
            '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4',
            '\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x3', '\x2', '\x5',
            '\x2', '&', '\n', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\a',
            '\x2', '+', '\n', '\x2', '\f', '\x2', '\xE', '\x2', '.', '\v', '\x2',
            '\x5', '\x2', '\x30', '\n', '\x2', '\x3', '\x3', '\x3', '\x3', '\x3',
            '\x3', '\x3', '\x3', '\x3', '\x3', '\x5', '\x3', '\x37', '\n', '\x3',
            '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x5', '\x3', '\x5',
            '\x3', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\a', '\x3',
            '\a', '\x3', '\a', '\a', '\a', '\x45', '\n', '\a', '\f', '\a', '\xE',
            '\a', 'H', '\v', '\a', '\x3', '\b', '\x3', '\b', '\x5', '\b', 'L', '\n',
            '\b', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\n', '\x3', '\n',
            '\x3', '\n', '\a', '\n', 'T', '\n', '\n', '\f', '\n', '\xE', '\n', 'W',
            '\v', '\n', '\x3', '\v', '\x3', '\v', '\x3', '\v', '\x3', '\f', '\x3',
            '\f', '\x3', '\f', '\a', '\f', '_', '\n', '\f', '\f', '\f', '\xE', '\f',
            '\x62', '\v', '\f', '\x3', '\r', '\x5', '\r', '\x65', '\n', '\r', '\x3',
            '\r', '\x3', '\r', '\x3', '\r', '\x3', '\r', '\x3', '\r', '\x5', '\r',
            'l', '\n', '\r', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE', '\x3', '\xE',
            '\x3', '\xF', '\x3', '\xF', '\x3', '\xF', '\x5', '\xF', 'u', '\n', '\xF',
            '\x3', '\x10', '\x3', '\x10', '\x3', '\x11', '\x3', '\x11', '\x3', '\x12',
            '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\a', '\x12',
            '\x80', '\n', '\x12', '\f', '\x12', '\xE', '\x12', '\x83', '\v', '\x12',
            '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x2', '\x2', '\x13', '\x2',
            '\x4', '\x6', '\b', '\n', '\f', '\xE', '\x10', '\x12', '\x14', '\x16',
            '\x18', '\x1A', '\x1C', '\x1E', ' ', '\"', '\x2', '\a', '\x3', '\x2',
            '\x16', '\x17', '\x3', '\x2', '\r', '\xE', '\x3', '\x2', '\x10', '\x15',
            '\x3', '\x2', '\x1C', '%', '\x3', '\x2', '\x18', '\x1B', '\x2', '\x85',
            '\x2', '/', '\x3', '\x2', '\x2', '\x2', '\x4', '\x36', '\x3', '\x2', '\x2',
            '\x2', '\x6', '\x38', '\x3', '\x2', '\x2', '\x2', '\b', ';', '\x3', '\x2',
            '\x2', '\x2', '\n', '>', '\x3', '\x2', '\x2', '\x2', '\f', '\x41', '\x3',
            '\x2', '\x2', '\x2', '\xE', 'I', '\x3', '\x2', '\x2', '\x2', '\x10', 'M',
            '\x3', '\x2', '\x2', '\x2', '\x12', 'P', '\x3', '\x2', '\x2', '\x2', '\x14',
            'X', '\x3', '\x2', '\x2', '\x2', '\x16', '[', '\x3', '\x2', '\x2', '\x2',
            '\x18', '\x64', '\x3', '\x2', '\x2', '\x2', '\x1A', 'm', '\x3', '\x2',
            '\x2', '\x2', '\x1C', 't', '\x3', '\x2', '\x2', '\x2', '\x1E', 'v', '\x3',
            '\x2', '\x2', '\x2', ' ', 'x', '\x3', '\x2', '\x2', '\x2', '\"', 'z',
            '\x3', '\x2', '\x2', '\x2', '$', '&', '\a', '\x3', '\x2', '\x2', '%',
            '$', '\x3', '\x2', '\x2', '\x2', '%', '&', '\x3', '\x2', '\x2', '\x2',
            '&', '\'', '\x3', '\x2', '\x2', '\x2', '\'', ',', '\x5', '\x4', '\x3',
            '\x2', '(', ')', '\a', '\x4', '\x2', '\x2', ')', '+', '\x5', '\x4', '\x3',
            '\x2', '*', '(', '\x3', '\x2', '\x2', '\x2', '+', '.', '\x3', '\x2', '\x2',
            '\x2', ',', '*', '\x3', '\x2', '\x2', '\x2', ',', '-', '\x3', '\x2', '\x2',
            '\x2', '-', '\x30', '\x3', '\x2', '\x2', '\x2', '.', ',', '\x3', '\x2',
            '\x2', '\x2', '/', '%', '\x3', '\x2', '\x2', '\x2', '/', '\x30', '\x3',
            '\x2', '\x2', '\x2', '\x30', '\x3', '\x3', '\x2', '\x2', '\x2', '\x31',
            '\x37', '\x5', '\x6', '\x4', '\x2', '\x32', '\x37', '\x5', '\b', '\x5',
            '\x2', '\x33', '\x37', '\x5', '\n', '\x6', '\x2', '\x34', '\x37', '\x5',
            '\x10', '\t', '\x2', '\x35', '\x37', '\x5', '\x14', '\v', '\x2', '\x36',
            '\x31', '\x3', '\x2', '\x2', '\x2', '\x36', '\x32', '\x3', '\x2', '\x2',
            '\x2', '\x36', '\x33', '\x3', '\x2', '\x2', '\x2', '\x36', '\x34', '\x3',
            '\x2', '\x2', '\x2', '\x36', '\x35', '\x3', '\x2', '\x2', '\x2', '\x37',
            '\x5', '\x3', '\x2', '\x2', '\x2', '\x38', '\x39', '\a', '\x5', '\x2',
            '\x2', '\x39', ':', '\a', '\x1C', '\x2', '\x2', ':', '\a', '\x3', '\x2',
            '\x2', '\x2', ';', '<', '\a', '\x6', '\x2', '\x2', '<', '=', '\a', '\x1C',
            '\x2', '\x2', '=', '\t', '\x3', '\x2', '\x2', '\x2', '>', '?', '\a', '\a',
            '\x2', '\x2', '?', '@', '\x5', '\f', '\a', '\x2', '@', '\v', '\x3', '\x2',
            '\x2', '\x2', '\x41', '\x46', '\x5', '\xE', '\b', '\x2', '\x42', '\x43',
            '\a', '\b', '\x2', '\x2', '\x43', '\x45', '\x5', '\xE', '\b', '\x2', '\x44',
            '\x42', '\x3', '\x2', '\x2', '\x2', '\x45', 'H', '\x3', '\x2', '\x2',
            '\x2', '\x46', '\x44', '\x3', '\x2', '\x2', '\x2', '\x46', 'G', '\x3',
            '\x2', '\x2', '\x2', 'G', '\r', '\x3', '\x2', '\x2', '\x2', 'H', '\x46',
            '\x3', '\x2', '\x2', '\x2', 'I', 'K', '\a', '&', '\x2', '\x2', 'J', 'L',
            '\t', '\x2', '\x2', '\x2', 'K', 'J', '\x3', '\x2', '\x2', '\x2', 'K',
            'L', '\x3', '\x2', '\x2', '\x2', 'L', '\xF', '\x3', '\x2', '\x2', '\x2',
            'M', 'N', '\a', '\t', '\x2', '\x2', 'N', 'O', '\x5', '\x12', '\n', '\x2',
            'O', '\x11', '\x3', '\x2', '\x2', '\x2', 'P', 'U', '\a', '&', '\x2', '\x2',
            'Q', 'R', '\a', '\b', '\x2', '\x2', 'R', 'T', '\a', '&', '\x2', '\x2',
            'S', 'Q', '\x3', '\x2', '\x2', '\x2', 'T', 'W', '\x3', '\x2', '\x2', '\x2',
            'U', 'S', '\x3', '\x2', '\x2', '\x2', 'U', 'V', '\x3', '\x2', '\x2', '\x2',
            'V', '\x13', '\x3', '\x2', '\x2', '\x2', 'W', 'U', '\x3', '\x2', '\x2',
            '\x2', 'X', 'Y', '\a', '\n', '\x2', '\x2', 'Y', 'Z', '\x5', '\x16', '\f',
            '\x2', 'Z', '\x15', '\x3', '\x2', '\x2', '\x2', '[', '`', '\x5', '\x18',
            '\r', '\x2', '\\', ']', '\t', '\x3', '\x2', '\x2', ']', '_', '\x5', '\x18',
            '\r', '\x2', '^', '\\', '\x3', '\x2', '\x2', '\x2', '_', '\x62', '\x3',
            '\x2', '\x2', '\x2', '`', '^', '\x3', '\x2', '\x2', '\x2', '`', '\x61',
            '\x3', '\x2', '\x2', '\x2', '\x61', '\x17', '\x3', '\x2', '\x2', '\x2',
            '\x62', '`', '\x3', '\x2', '\x2', '\x2', '\x63', '\x65', '\a', '\xF',
            '\x2', '\x2', '\x64', '\x63', '\x3', '\x2', '\x2', '\x2', '\x64', '\x65',
            '\x3', '\x2', '\x2', '\x2', '\x65', 'k', '\x3', '\x2', '\x2', '\x2', '\x66',
            'l', '\x5', '\x1A', '\xE', '\x2', 'g', 'h', '\a', '\v', '\x2', '\x2',
            'h', 'i', '\x5', '\x16', '\f', '\x2', 'i', 'j', '\a', '\f', '\x2', '\x2',
            'j', 'l', '\x3', '\x2', '\x2', '\x2', 'k', '\x66', '\x3', '\x2', '\x2',
            '\x2', 'k', 'g', '\x3', '\x2', '\x2', '\x2', 'l', '\x19', '\x3', '\x2',
            '\x2', '\x2', 'm', 'n', '\x5', '\x1C', '\xF', '\x2', 'n', 'o', '\t', '\x4',
            '\x2', '\x2', 'o', 'p', '\x5', '\x1C', '\xF', '\x2', 'p', '\x1B', '\x3',
            '\x2', '\x2', '\x2', 'q', 'u', '\x5', '\x1E', '\x10', '\x2', 'r', 'u',
            '\x5', ' ', '\x11', '\x2', 's', 'u', '\x5', '\"', '\x12', '\x2', 't',
            'q', '\x3', '\x2', '\x2', '\x2', 't', 'r', '\x3', '\x2', '\x2', '\x2',
            't', 's', '\x3', '\x2', '\x2', '\x2', 'u', '\x1D', '\x3', '\x2', '\x2',
            '\x2', 'v', 'w', '\a', '&', '\x2', '\x2', 'w', '\x1F', '\x3', '\x2', '\x2',
            '\x2', 'x', 'y', '\t', '\x5', '\x2', '\x2', 'y', '!', '\x3', '\x2', '\x2',
            '\x2', 'z', '{', '\t', '\x6', '\x2', '\x2', '{', '|', '\a', '\v', '\x2',
            '\x2', '|', '\x81', '\x5', '\x1C', '\xF', '\x2', '}', '~', '\a', '\b',
            '\x2', '\x2', '~', '\x80', '\x5', '\x1C', '\xF', '\x2', '\x7F', '}', '\x3',
            '\x2', '\x2', '\x2', '\x80', '\x83', '\x3', '\x2', '\x2', '\x2', '\x81',
            '\x7F', '\x3', '\x2', '\x2', '\x2', '\x81', '\x82', '\x3', '\x2', '\x2',
            '\x2', '\x82', '\x84', '\x3', '\x2', '\x2', '\x2', '\x83', '\x81', '\x3',
            '\x2', '\x2', '\x2', '\x84', '\x85', '\a', '\f', '\x2', '\x2', '\x85',
            '#', '\x3', '\x2', '\x2', '\x2', '\xE', '%', ',', '/', '\x36', '\x46',
            'K', 'U', '`', '\x64', 'k', 't', '\x81',
        };

        public static readonly ATN _ATN =
            new ATNDeserializer().Deserialize(_serializedATN);


    }
}
