using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PAC.Domain;
using PAC.IBusinessLogic;
using PAC.WebAPI.Filters;

namespace PAC.WebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentLogic _studentLogic;

        public StudentController(IStudentLogic studentLogic)
        {
            this._studentLogic = studentLogic;
        }

        [HttpGet]
        public IActionResult GeAll()
        {
            var students = _studentLogic.GetStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = _studentLogic.GetStudentById(id);
            return Ok(student);
        }

        [AuthorizationFilter]
        [HttpPost]
        public IActionResult Post([FromBody] Student value)
        {
            if (value.Id >0 && value.Name != null) {
                _studentLogic.InsertStudents(value);
                return Ok();
            } else
            {
                return NotFound();
            }
          
        }
    }
}
