using DuluxBrief.Server.Entities;

namespace DuluxBrief.Server.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext studentDbContext;
        public StudentService(StudentDbContext studentDbContext)
        {
            this.studentDbContext = studentDbContext;
        }

        public async Task AddStudentAsync(Student student)
        {
            await studentDbContext.Students.AddAsync(student);
            await studentDbContext.SaveChangesAsync();
        }

    }
}