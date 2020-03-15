namespace WebFeatures.QueryFilters.Nodes.Base
{
    internal abstract class AggregateNode : BaseNode
    {
        protected readonly BaseNode Left;
        protected readonly BaseNode Right;

        protected AggregateNode(BaseNode left, BaseNode right)
        {
            Left = left;
            Right = right;
        }
    }
}
