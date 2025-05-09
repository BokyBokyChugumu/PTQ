using PTQ.Models;


namespace PTQ.Repositories
{
    public interface IPotatoTeacherRepository
    {
        Task<PotatoTeacher?> GetByNameAsync(string name);
        Task<PotatoTeacher> AddAsync(PotatoTeacher teacher);
        Task<PotatoTeacher?> GetByIdAsync(int id);
    }
}