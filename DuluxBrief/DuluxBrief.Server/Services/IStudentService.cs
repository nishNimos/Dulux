using DuluxBrief.Server.Entities;
namespace DuluxBrief.Server.Services
{
    public interface IStudentService
    {
        Task AddStudentAsync(Student student);
    }
}