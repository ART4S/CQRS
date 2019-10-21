using LinqToQuerystring;
using System.Collections.Generic;
using System.Linq;
using WebFeatures.QueryFiltering.Exceptions;
using WebFeatures.QueryFiltering.Filters;
using WebFeatures.QueryFiltering.Results;

namespace WebFeatures.QueryFiltering.Utils
{
    public static class FilteringExtensions
    {
        public static FilteringResult ApplyFilter<T>(this IQueryable<T> sourceQueryable, QueryFilter filter)
        {
            try
            {
                var queryString = filter.BuildQueryString();
                var records = sourceQueryable.LinqToQuerystring(queryString).ToList();

                return new FilteringResult(records);
            }
            catch
            {
                throw new FilteringException("Ошибка во время фильтрации результата");
            }
        }

        private static string BuildQueryString(this QueryFilter filter)
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