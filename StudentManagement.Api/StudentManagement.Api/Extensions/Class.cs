using StudentManagement.Api.Model;
using StudentManagement.Repository;

namespace StudentManagement.Api.Extensions
{
    /// <summary>
    /// Extension methods for calculating and getting grades based on marks.
    /// </summary>
    public static class Class
    {
        /// <summary>
        /// Calculates the average mark from a collection of marks.
        /// </summary>
        /// <param name="MarkValue">The collection of marks.</param>
        /// <returns>The average mark.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the list of marks is empty.</exception>
        public static decimal CalculateAverage(this IEnumerable<int> MarkValue)
        {
            if (MarkValue == null || !MarkValue.Any())
                throw new InvalidOperationException("The list of marks is empty.");

            return MarkValue.Sum() / MarkValue.Count();
        }

        /// <summary>
        /// Gets the grade based on the average mark.
        /// </summary>
        /// <param name="averageMark">The average mark.</param>
        /// <returns>The corresponding grade.</returns>
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

        /// <summary>
        /// Calculates the sum of marks from a collection.
        /// </summary>
        /// <param name="marks">The collection of marks.</param>
        /// <returns>The sum of marks.</returns>
        public static int CalculateSum(this IEnumerable<int> marks)
        {
            return marks.Sum();
        }

        /// <summary>
        /// Tries to get the highest mark for a given course.
        /// </summary>
        public static async Task<(int MarkValue, string StudentName)> TryGetHighestMarkForCourse(this IRepository<Mark> markRepository, int courseId, IRepository<Student> studentRepository)
        {
            var marks = await markRepository.GetAll();
            marks = marks.Where(m => m.CourseId == courseId).ToList();

            if (!marks.Any())
            {
                return (0, string.Empty);
            }

            var highestMark = marks.OrderByDescending(m => m.MarkValue).First();
            var student = await studentRepository.GetById(highestMark.StudentId);

            return (highestMark.MarkValue, student.StudentName);
        }

        /// <summary>
        /// Tries to get the lowest mark for a given course.
        /// </summary>
        public static async Task<(int MarkValue, string StudentName)> TryGetLowestMarkForCourse(this IRepository<Mark> markRepository, int courseId, IRepository<Student> studentRepository)
        {
            var marks = await markRepository.GetAll();
            marks = marks.Where(m => m.CourseId == courseId).ToList();

            if (!marks.Any())
            {
                return (0, string.Empty);
            }

            var lowestMark = marks.OrderBy(m => m.MarkValue).First();
            var student = await studentRepository.GetById(lowestMark.StudentId);

            return (lowestMark.MarkValue, student.StudentName);
        }


    }
}
