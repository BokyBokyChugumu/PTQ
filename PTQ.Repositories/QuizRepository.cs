using Microsoft.EntityFrameworkCore;
using PTQ.Models;

namespace PTQ.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ApplicationDbContext _context;

        public QuizRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return await _context.Quizzes.ToListAsync();
        }

        public async Task<Quiz?> GetByIdAsync(int id)
        {
            return await _context.Quizzes
                .Include(q => q.PotatoTeacher) 
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Quiz> AddAsync(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
            return quiz;
        }
    }
}