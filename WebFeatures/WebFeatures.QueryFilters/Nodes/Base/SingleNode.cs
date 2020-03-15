namespace WebFeatures.QueryFilters.Nodes.Base
{
    internal abstract class SingleNode : BaseNode
    {
        protected readonly string Value;

        protected SingleNode(string value)
        {
            Value = value;
        }
    }
}
