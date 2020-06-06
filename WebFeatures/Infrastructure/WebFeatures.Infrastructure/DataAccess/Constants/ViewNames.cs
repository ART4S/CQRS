using System.Linq;

namespace WebFeatures.Infrastructure.DataAccess.Constants
{
    internal static class ViewNames
    {
        public static string[] All = Products.All.ToArray();

        public static class Products
        {
            public static string[] All =
            {
                GET_PRODUCTS_LIST,
                GET_PRODUCT_COMMENTS,
                GET_PRODUCT_REVIEWS
            };

            public const string GET_PRODUCTS_LIST = "get_products_list";
            public const string GET_PRODUCT_COMMENTS = "get_product_comments";
            public const string GET_PRODUCT_REVIEWS = "get_product_reviews";
        }
    }
}
