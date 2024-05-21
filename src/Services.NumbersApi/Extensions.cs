namespace Services.NumbersApi
{
    internal static class Extensions
    {
        public static string ForQuery(this CategoryFact category)
        {
            return category.ToString().ToLower();
        }
    }
}
