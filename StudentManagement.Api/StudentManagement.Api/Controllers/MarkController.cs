using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Extensions;
using StudentManagement.Api.Model;
using StudentManagement.Repository;

/// <summary>
/// Controller for managing marks of students.
/// </summary>
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

    /// <summary>
    /// Adds a mark for a student in a course.
    /// </summary>
    /// <param name="studentId">The ID of the student.</param>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="markValue">The value of the mark.</param>
    /// <returns>The ID of the created mark.</returns>
    [HttpPost("student/{studentId}/course/{courseId}")]
    public async Task<IActionResult> AddMarkForStudent(int studentId, int courseId, [FromBody] int markValue)
    {
        var student = await _studentRepository.GetById(studentId);

        if (student == null)
        {
            return NotFound("Student not found");
        }

        var course = await _courseRepository.GetById(courseId);

        if (course == null)
        {
            return NotFound("Course not found");
        }

        var mark = new Mark
        {
            StudentId = studentId,
            CourseId = courseId,
            MarkValue = markValue,
            CreatedOn = DateTime.UtcNow,
            LastModifiedOn = DateTime.UtcNow
        };

        var createdMark = await _markRepository.Create(mark);
        return Ok(createdMark.Id);
    }

    /// <summary>
    /// Retrieves marks for a student.
    /// </summary>
    /// <param name="studentId">The ID of the student.</param>
    /// <returns>The marks of the student.</returns>
    [HttpGet("student/{studentId}/marks")]
    public async Task<IActionResult> GetMarksByStudentId(int studentId)
    {
        var marks = await _markRepository.GetAll();

        marks = marks.Where(m => m.StudentId == studentId).ToList();

        if (!marks.Any())
        {
            return NotFound("No marks found for the student.");
        }

        var markModels = marks.Select(m => new
        {
            StudentId = m.StudentId,
            CourseId = m.CourseId,
            MarkValue = m.MarkValue,
            CreatedOn = DateTime.UtcNow,
            LastModifiedOn = DateTime.UtcNow
        }).ToList();

        return Ok(markModels);
    }

    /// <summary>
    /// Retrieves details of a student including their average mark, total marks, and grade.
    /// </summary>
    /// <param name="studentId">The ID of the student.</param>
    /// <returns>The details of the student.</returns>
    [HttpGet("student/{studentId}/details")]
    public async Task<IActionResult> GetStudentDetails(int studentId)
    {
        var student = await _studentRepository.GetById(studentId);
        var studentName = student.StudentName;

        var marks = await _markRepository.GetAll();
        marks = marks.Where(m => m.StudentId == studentId).ToList();

        if (!marks.Any())
        {
            return NotFound("No marks found for the student.");
        }

        var averageMark = marks.Select(x => x.MarkValue).CalculateAverage();
        var totalMarks = marks.Select(x => x.MarkValue).CalculateSum();

        var studentDetails = new
        {
            StudentId = studentId,
            StudentName = studentName,
            AverageMark = averageMark,
            TotalMarks = totalMarks,
            Grade = averageMark.GetGrade()
        };

        return Ok(studentDetails);
    }

    /// <summary>
    /// Deletes a mark for a student in a course.
    /// </summary>
    /// <param name="studentId">The ID of the student.</param>
    /// <param name="courseId">The ID of the course.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpDelete("student/{studentId}/course/{courseId}")]
    public async Task<IActionResult> DeleteMarkForStudent(int studentId, int courseId)
    {
        var marks = await _markRepository.GetAll();
        var mark = marks.FirstOrDefault(m => m.StudentId == studentId && m.CourseId == courseId);

        if (mark == null)
        {
            return NotFound("Mark not found");
        }
        await _markRepository.Delete(mark.Id);
        return Ok("Mark deleted successfully");
    }

    /// <summary>
    /// Updates the mark value for a student in a course.
    /// </summary>
    /// <param name="studentId">The ID of the student.</param>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="markValue">The new value of the mark.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpPut("student/{studentId}/course/{courseId}")]
    public async Task<IActionResult> UpdateMarkForStudent(int studentId, int courseId, [FromBody] int markValue)
    {
        var student = await _studentRepository.GetById(studentId);

        if (student == null)
        {
            return NotFound("Student not found");
        }

        var course = await _courseRepository.GetById(courseId);

        if (course == null)
        {
            return NotFound("Course not found");
        }

        var marks = await _markRepository.GetAll();
        var mark = marks.FirstOrDefault(m => m.StudentId == studentId && m.CourseId == courseId);

        if (mark == null)
        {
            return NotFound("Mark not found");
        }

        mark.MarkValue = markValue;
        mark.LastModifiedOn = DateTime.UtcNow;
        await _markRepository.Update(mark);

        return Ok("Mark updated successfully");
    }
}

