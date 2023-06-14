using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Model;
using StudentManagement.Repository;

[ApiController]
[Route("[controller]")]
public class MarkController : ControllerBase
{
    private readonly IRepository<Mark> _markRepository;
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<Course> _courseRepository;

    public MarkController(IRepository<Mark> markRepository, IRepository<Student> studentRepository, IRepository<Course> courseRepository)
    {
        _markRepository = markRepository;
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
    }

    [HttpPost("student/{studentId}/course/{courseId}")]
    public async Task<IActionResult> AddMarkForStudent(int studentId, int courseId, [FromBody] int markValue)
    {
        // Get the student with the specified studentId
        var student = await _studentRepository.GetById(studentId);

        if (student == null)
        {
            // If no student found, return not found status
            return NotFound("Student not found");
        }

        // Get the course with the specified courseId
        var course = await _courseRepository.GetById(courseId);

        if (course == null)
        {
            // If no course found, return not found status
            return NotFound("Course not found");
        }

        // Create the mark with the given mark value
        var mark = new Mark
        {
            StudentId = studentId,
            CourseId = courseId,
            MarkValue = markValue
        };

        // Create the mark
        var createdMark = await _markRepository.Create(mark);
        return Ok(createdMark.Id);
    }

}

