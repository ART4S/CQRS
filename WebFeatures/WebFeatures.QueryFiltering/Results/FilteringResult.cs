using System.Collections;

namespace WebFeatures.QueryFiltering.Results
{
    public class FilteringResult
    {
        public int Count { get; }

        public ICollection Items { get; }

        public FilteringResult(ICollection items)
        {
            Count = items.Count;
            Items = items;
        }
    }
}
