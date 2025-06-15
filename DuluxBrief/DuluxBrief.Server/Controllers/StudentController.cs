using DuluxBrief.Server.Entities;
using DuluxBrief.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace DuluxBrief.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;


        public StudentController(ILogger<StudentController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }
        [HttpPost]
        [Route("AddStudent")]
        public async Task<IActionResult> AddStudent(StudentData studentData)
        {
            //store transcript file somewhere and get the url (Azure blob, AWS S3, etc.)
            var url = "dummyUrl"; //To replace with actual file storage logic
            Student student = new Student
            {
                Name = studentData.student.Name,
                Email = studentData.student.Email,
                TranscriptUrl = url,
            };
            await _studentService.AddStudentAsync(student);
            _logger.LogInformation("Student added successfully with ID: {Id}", student.Id);
            return Ok();

        }
        [HttpGet]
        [Route("GetStudents")]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }
    }
    public class StudentData
    {
        public Student student { get; set; }
        public IFormFile file { get; set; }
    }
}
