namespace StudentManagement.Api.Extensions
{
    public static class Class
    {
        public static decimal CalculateAverage(this IEnumerable<int> MarkValue)
        {
            if (MarkValue == null || !MarkValue.Any())
                throw new InvalidOperationException("The list of marks is empty.");

            return MarkValue.Sum() / MarkValue.Count();
        }
    }
}
