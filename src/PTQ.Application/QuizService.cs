using PTQ.Models;
using PTQ.Repositories;



namespace PTQ.Application
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IPotatoTeacherRepository _teacherRepository;
        private readonly ApplicationDbContext _dbContext;

        public QuizService(IQuizRepository quizRepository, IPotatoTeacherRepository teacherRepository, ApplicationDbContext dbContext)
        {
            _quizRepository = quizRepository;
            _teacherRepository = teacherRepository;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<QuizSummaryDto>> GetAllQuizzesAsync()
        {
            var quizzes = await _quizRepository.GetAllAsync();
            return quizzes.Select(q => new QuizSummaryDto
            {
                Id = q.Id,
                Name = q.Name
            });
        }

        public async Task<QuizDetailDto?> GetQuizByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetByIdAsync(id);
            if (quiz == null)
            {
                return null;
            }

            return new QuizDetailDto
            {
                Id = quiz.Id,
                Name = quiz.Name,
                PotatoTeacherName = quiz.PotatoTeacher?.Name ?? "Unknown Teacher",
                Path = quiz.PathFile
            };
        }

        public async Task<QuizDetailDto> CreateQuizAsync(CreateQuizRequestDto createQuizDto)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var teacher = await _teacherRepository.GetByNameAsync(createQuizDto.PotatoTeacherName);
                if (teacher == null)
                {
                    teacher = new PotatoTeacher { Name = createQuizDto.PotatoTeacherName };
                    await _teacherRepository.AddAsync(teacher);
                    await _dbContext.SaveChangesAsync();
                }

                var quiz = new Quiz
                {
                    Name = createQuizDto.Name,
                    PotatoTeacherId = teacher.Id,
                    PathFile = createQuizDto.Path
                };

                await _quizRepository.AddAsync(quiz);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return new QuizDetailDto
                {
                    Id = quiz.Id,
                    Name = quiz.Name,
                    PotatoTeacherName = teacher.Name,
                    Path = quiz.PathFile
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}