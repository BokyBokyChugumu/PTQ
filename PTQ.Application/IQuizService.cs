

namespace PTQ.Application
{
    public interface IQuizService
    {
        Task<IEnumerable<QuizSummaryDto>> GetAllQuizzesAsync();
        Task<QuizDetailDto?> GetQuizByIdAsync(int id);
        Task<QuizDetailDto> CreateQuizAsync(CreateQuizRequestDto createQuizDto);
    }
}