using CRUD_Operation_Using_Ado.Net.Model;
using CRUD_Operation_Using_Ado.Net.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operation_Using_Ado.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        public StudentController(IStudentServices studentServices)
        {
            this._studentServices = studentServices;
        }
        [HttpGet("GetStudets")]
        public IActionResult GetStudents()
        {
            if (_studentServices.ViewStudent() == null)
                return BadRequest("Internal Server Error");
            return Ok(_studentServices.ViewStudent());
        }
        [HttpPost("AddStudent")]
        public IActionResult Addstudents(Student std)
        {
            if (std.Id == 0)
                return BadRequest("Id must not Equal to Zero");
             _studentServices.AddStudent(std);
            return Ok("Student Added");
        }
        [HttpPut("UpdateStudent")]
        public IActionResult UpdateStudent(Student std)
        {
            if(std.Id == 0)
                return BadRequest("Id must not Equal to Zero");
            _studentServices.UpdateStudent(std);
             return Ok("Updated Student");
        }
        [HttpDelete("DeleteStudent")]
        public IActionResult DeleteStudent(int id)
        {
            if(id == 0)
                return BadRequest("Id must not Equal to Zero");
            _studentServices.DeleteStudent(id);
            return Ok("Deleted Student");
        }
    }
}
