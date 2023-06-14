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

        public static string GetGrade(this decimal averageMark)
        {
            if (averageMark >= 90)
                return "A";
            else if (averageMark >= 80)
                return "B";
            else if (averageMark >= 70)
                return "C";
            else if (averageMark >= 60)
                return "D";
            else if (averageMark >= 50)
                return "E";
            else
                return "F";
        }
    }
}
