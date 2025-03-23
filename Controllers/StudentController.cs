using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiClass.Data;
using webApiClass.DTO;
using webApiClass.Iservice;
using webApiClass.model;

namespace webApiClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentController : ControllerBase
    {
        private readonly IStudent studentService;
        private readonly ILogger<StudentController> logger;

        public StudentController(IStudent studentService)
        {
            this.studentService = studentService;
        }

        [HttpPost]
        [Route("CreateStudents")]

        public IActionResult CreateStudent([FromBody]StudentDTO student)
        {
          try
            {
                studentService.CreateStudent(student);
                return Ok("Student successfully Created");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }    
        }
        [Authorize("User")]
        [HttpGet("GetAllStudent")]

        public IActionResult GetAllStudents()
        {
            var students = studentService.GetAllStudents();
            logger.LogInformation("This is the list of all students:{@students}", students);
            return Ok(students);
        }

        [Authorize("Admin")]
        [HttpGet("GetStudentById")]

        public IActionResult GetStudentById(int id) 
        {
            var Id = studentService.GetStudentById(id);
            if (Id == null)
            {
                return NotFound("student Not found");
            }

            return Ok(Id);
        }

        [HttpGet("GetStudentByName")]
        public IActionResult GetStudentByName(string name)
        {
            var Name = studentService.GetStudentByName(name);
            if (Name == null)
            {
                return NotFound("student Not found");
            }
            return Ok(Name);
        }

        [HttpDelete("DeleteStudentById")]

        public IActionResult DeleteStudentById(int id)
        {
            var Id = studentService.DeleteStudentById(id);
            if (Id == null)
            {
                return NotFound("Id not found");
            }
            else return Ok("");

           
            
        }
        [HttpPut("updateStudentById")]
        public IActionResult UpdateStudentId(int id,[FromBody] Student update)
        {
            var student = studentService.GetStudentById(id);

            if (student == null)
            {
                return NotFound("Student not found"); 
            }
            studentService.UpdateStudentId(id,update);
            return Ok("student updated successefully");
        }

    }
}
