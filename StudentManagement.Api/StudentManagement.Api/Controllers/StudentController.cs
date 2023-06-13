using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Model;
using StudentManagement.Repository;


[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly IRepository<Student> StudentRepository;

    public StudentController(IRepository<Student> _studentRepository)
    {
        StudentRepository = _studentRepository;
    }

    /// <summary>
    /// Endpoint to fetch details of an Students.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        var students = await StudentRepository.GetAll();
        return Ok(students);
    }

    /// <summary>
    /// Adding student data to the database
    /// </summary>
    /// <returns>
    /// Id of inserted record
    /// </returns>    
    [HttpPost]
    public async Task<IActionResult> AddStudent(Student student)
    {
        var createdStudent = await StudentRepository.Create(student);
        return Ok(createdStudent.Id);
    }

    /// <summary>
    /// Endpoint to delete a student by ID.
    /// </summary>
    /// <param name="id">students Id to fetch student's data</param>
    [HttpDelete("student/{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var deletedStudent = await StudentRepository.Delete(id);
        if (deletedStudent == null)
        {
            return NotFound();
        }

        return Ok(deletedStudent.Id);
    }

    /// <summary>
    /// Endpoint to fetch details of an student with given id.
    /// </summary>
    /// <param name="id">Customers's Id to fetch student's data</param>
    [HttpGet("student/{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await StudentRepository.GetById(id);
        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }

    /// <summary>
    /// Endpoint to update student record
    /// </summary>
    /// <param name="student">
    /// customer contains the updated student's data
    /// </param>
    /// <returns> 
    /// student id of updated record 
    /// </returns>
    [HttpPut]
    public async Task<IActionResult> UpadteCustomer(Student student)
    {
        var updatedStudent = await StudentRepository.Update(student);
        return Ok(updatedStudent.Id);
    }

}