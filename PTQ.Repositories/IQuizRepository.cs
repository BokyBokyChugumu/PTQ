using PTQ.Models;

namespace PTQ.Repositories
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetAllAsync();
        Task<Quiz?> GetByIdAsync(int id);
        Task<Quiz> AddAsync(Quiz quiz);
    }
}