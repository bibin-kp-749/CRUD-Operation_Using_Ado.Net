using CRUD_Operation_Using_Ado.Net.Model;
using CRUD_Operation_Using_Ado.Net.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operation_Using_Ado.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //Implementing dependencyInjection
        private readonly IStudentServices _studentServices;
        public StudentController(IStudentServices studentServices)
        {
            this._studentServices = studentServices;
        }
        [HttpGet("GetStudets")]
        // method for Get all Students 
        public IActionResult GetStudents()
        {
            if (_studentServices.ViewStudent() == null)
                return BadRequest("Internal Server Error");
            return Ok(_studentServices.ViewStudent());
        }
        [HttpPost("AddStudent")] 
        //method for Add student
        public IActionResult Addstudents(Student std)
        {
            if (std.Id == 0)
                return BadRequest("Id must not Equal to Zero");
             _studentServices.AddStudent(std);
            return Ok("Student Added");
        }
        [HttpPut("UpdateStudent")]
        //method for updating the data of existing student
        public IActionResult UpdateStudent(Student std)
        {
            if(std.Id == 0)
                return BadRequest("Id must not Equal to Zero");
            _studentServices.UpdateStudent(std);
             return Ok("Updated Student");
        }
        [HttpDelete("DeleteStudent")]
        //method for deleting an existing student by id
        public IActionResult DeleteStudent(int id)
        {
            if(id == 0)
                return BadRequest("Id must not Equal to Zero");
            _studentServices.DeleteStudent(id);
            return Ok("Deleted Student");
        }
    }
}
