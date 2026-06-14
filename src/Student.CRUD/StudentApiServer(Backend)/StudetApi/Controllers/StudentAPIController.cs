
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPIBusinesslayer;
using StudentAPIDataAccessLayer;

namespace StudetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        [HttpGet("All",Name ="GetAllStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {
            List<StudentDTO> studentList = StudentAPIBusinesslayer.Student.GetAllStudents();
if(studentList == null || studentList.Count == 0)
            {
                return NotFound("No Students Found");
            }
            return Ok(studentList);

        }
        [HttpGet("Passes", Name = "GetPassesStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetPassesStudents()
        {
            List<StudentDTO> studentList = StudentAPIBusinesslayer.Student.GetPassesStudents();
            if (studentList == null || studentList.Count == 0)
            {
                return NotFound("No Students Found");
            }
            return Ok(studentList);
        }
        [HttpGet("Average", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<float> GetAverageGrade()
        {
            float average = StudentAPIBusinesslayer.Student.GetAverageGrade();
            if (average == 0)
            {
                return NotFound("No Students Found");
            }
            return Ok(average);
        }
      
   
        [HttpGet("GetById/{id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            Student student = Student.Find(id);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }
            StudentDTO SDTO = student.SDTO;
            return Ok(SDTO);
        }

        [HttpPost("Add", Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> AddStudent(StudentDTO newStudentDTO)
        {
            //we validate the data here
            if (newStudentDTO == null || string.IsNullOrEmpty(newStudentDTO.Name) || newStudentDTO.Age < 0 || newStudentDTO.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }

            //newStudent.Id = StudentDataSimulation.StudentsList.Count > 0 ? StudentDataSimulation.StudentsList.Max(s => s.Id) + 1 : 1;

           Student student = new Student(new StudentDTO(newStudentDTO.Id, newStudentDTO.Name, newStudentDTO.Age, newStudentDTO.Grade));
            student.Save();

            newStudentDTO.Id = student.Id;

            //we return the DTO only not the full student object
            //we dont return Ok here,we return createdAtRoute: this will be status code 201 created.
            return CreatedAtRoute("GetStudentById", new { id = newStudentDTO.Id }, newStudentDTO);

        }
        [HttpPut("Update/{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<StudentDTO> UpdateStudent(int id, StudentDTO updatedStudentDTO)
        {
            if (updatedStudentDTO == null || string.IsNullOrEmpty(updatedStudentDTO.Name) || updatedStudentDTO.Age < 0 || updatedStudentDTO.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }
            Student student = Student.Find(id);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }
            student.Name = updatedStudentDTO.Name;
            student.Age = updatedStudentDTO.Age;
            student.Grade = updatedStudentDTO.Grade;
            student.Save();
            return Ok(student.SDTO);
        }
        [HttpDelete("Delete/{id}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteStudent(int id)
        {
            Student student = Student.Find(id);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }
            student.DeleteStudent();
            return Ok("Student Deleted Successfully");
        }

       

    }
}
