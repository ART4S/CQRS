namespace WebFeatures.Infrastructure.DataAccess.Constants
{
    internal static class ViewNames
    {
        public static class Products
        {
            public static string[] All =
            {
                GET_PRODUCTS_LIST,
                GET_PRODUCT_COMMENTS
            };

            public const string GET_PRODUCTS_LIST = "get_products_list";
            public const string GET_PRODUCT_COMMENTS = "get_product_comments";
        }
    }
}
