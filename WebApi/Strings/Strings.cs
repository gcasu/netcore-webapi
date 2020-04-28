namespace WebApi.Strings
{
    public static class Strings
    {
        public const string CompanyNotFound = "It was not possible to find a company with id {0}.";
        public const string ProductNotFound = "It was not possible to find a product with id {0}.";
        public const string OrdersExceeded = "You can't do more than one order per day.";
        public const string OrderCreationFailed = "Something went wrong while creating the order. Please contact a system admin to receive assistance.";
        public const string StockExceeded = "You have exceeded the stock quantity for product {0}: {1} products left.";
        public const string MinimumTotalSpent = "Total spent must be minimum {0}.";
    }
}
