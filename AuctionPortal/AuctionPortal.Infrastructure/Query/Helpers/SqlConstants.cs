namespace AuctionPortal.Infrastructure.Query.Helpers
{
    public static class SqlConstants
    {
        public const string SelectFromClause = "SELECT * FROM ";
        public const string WhereClause = "WHERE ";
        public const string OrderByClause = "ORDER BY ";
        public const string Ascending = " ASC";
        public const string Descending = " DESC";
        public const string Or = " OR ";
        public const string And = " AND ";
        public const string OpenParenthesis = "(";
        public const string CloseParenthesis = ")";
        public const string SelectCountFromOpen = "SELECT COUNT (*) FROM (";
        public const string SelectCountFromClose = ") AS [TableCount]";

        public static string Offset(int page, int pageSize)
        {
            return $"ORDER BY ID_EXAMPLE OFFSET(({page} - 1) * {pageSize}) ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }
    }
}
