using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Model;
using StudentManagement.Repository;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class CourseController : ControllerBase
{
    private readonly IRepository<Course> CourseRepository;

    public CourseController(IRepository<Course> courseRepository)
    {
        CourseRepository = courseRepository;
    }

    /// <summary>
    /// Endpoint to fetch details of all courses.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await CourseRepository.GetAll();
        return Ok(courses);
    }

    /// <summary>
    /// Adding a new course to the database.
    /// </summary>
    /// <returns>
    /// Id of the inserted course.
    /// </returns>    
    [HttpPost]
    public async Task<IActionResult> AddCourse(Course course)
    {
        var createdCourse = await CourseRepository.Create(course);
        return Ok(createdCourse.Id);
    }

    /// <summary>
    /// Endpoint to delete a course by ID.
    /// </summary>
    /// <param name="id">Course ID to delete the course.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var deletedCourse = await CourseRepository.Delete(id);
        if (deletedCourse == null)
        {
            return NotFound();
        }

        return Ok(deletedCourse.Id);
    }

    /// <summary>
    /// Endpoint to fetch details of a course by ID.
    /// </summary>
    /// <param name="id">Course ID to fetch the course's data.</param>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseById(int id)
    {
        var course = await CourseRepository.GetById(id);
        if (course == null)
        {
            return NotFound();
        }

        return Ok(course);
    }
}
