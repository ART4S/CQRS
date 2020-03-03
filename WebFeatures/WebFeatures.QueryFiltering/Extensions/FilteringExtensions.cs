using LinqToQuerystring;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebFeatures.QueryFiltering.Exceptions;
using WebFeatures.QueryFiltering.Filters;
using WebFeatures.QueryFiltering.Results;

namespace WebFeatures.QueryFiltering.Extensions
{
    public static class FilteringExtensions
    {
        public static FilteringResult ApplyFilter<T>(this IQueryable<T> sourceQueryable, QueryFilter filter)
        {
            try
            {
                string queryString = BuildQueryString(filter);
                ICollection records = sourceQueryable.LinqToQuerystring(queryString).ToList();

                return new FilteringResult(records);
            }
            catch
            {
                throw new FilteringException("Error occurred while filtering result");
            }
        }

        private static string BuildQueryString(QueryFilter filter)
        {
            if (filter == null) return string.Empty;

            var filteringParams = new List<string>();

            if (filter.Top.HasValue && filter.Top.Value > 0)
            {
                filteringParams.Add($"$top={filter.Top.Value}");
            }

            if (filter.Skip.HasValue && filter.Skip.Value > 0)
            {
                filteringParams.Add($"$skip={filter.Skip.Value}");
            }

            if (filter.Filter != null)
            {
                filteringParams.Add($"$filter={filter.Filter}");
            }

            if (filter.OrderBy != null)
            {
                filteringParams.Add($"$orderby={filter.OrderBy}");
            }

            return string.Join("&", filteringParams);
        }
    }
}