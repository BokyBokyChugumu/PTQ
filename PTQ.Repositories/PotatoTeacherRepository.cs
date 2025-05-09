using Microsoft.EntityFrameworkCore;
using PTQ.Models;

namespace PTQ.Repositories
{
    public class PotatoTeacherRepository : IPotatoTeacherRepository
    {
        private readonly ApplicationDbContext _context;

        public PotatoTeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PotatoTeacher?> GetByNameAsync(string name)
        {
            return await _context.PotatoTeachers.FirstOrDefaultAsync(pt => pt.Name == name);
        }
        
        public async Task<PotatoTeacher?> GetByIdAsync(int id)
        {
            return await _context.PotatoTeachers.FindAsync(id);
        }

        public async Task<PotatoTeacher> AddAsync(PotatoTeacher teacher)
        {
            await _context.PotatoTeachers.AddAsync(teacher);
            return teacher;
        }
    }
}