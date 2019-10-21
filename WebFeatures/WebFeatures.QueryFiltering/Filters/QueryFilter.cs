using Microsoft.AspNetCore.Mvc;

namespace WebFeatures.QueryFiltering.Filters
{
    public class QueryFilter
    {
        [BindProperty(Name = "$skip", SupportsGet = true)]
        public int? Skip { get; set; }

        [BindProperty(Name = "$top", SupportsGet = true)]
        public int? Top { get; set; }

        [BindProperty(Name = "$filter", SupportsGet = true)]
        public string Filter { get; set; }

        [BindProperty(Name = "$orderby", SupportsGet = true)]
        public string OrderBy { get; set; }
    }
}
