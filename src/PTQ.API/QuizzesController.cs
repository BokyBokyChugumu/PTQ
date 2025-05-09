using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTQ.Application;
using PTQ;

namespace PTQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
        }

       
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<QuizSummaryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<QuizSummaryDto>>> GetAllQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizzesAsync();
            return Ok(quizzes);
        }

        
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(QuizDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<QuizDetailDto>> GetQuiz(int id)
        {
            var quiz = await _quizService.GetQuizByIdAsync(id);
            if (quiz == null)
            {
                
                return NotFound($"Quiz with ID {id} not found.");
            }
            return Ok(quiz);
        }

       
        [HttpPost]
        [ProducesResponseType(typeof(QuizDetailDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuizDetailDto>> CreateQuiz([FromBody] CreateQuizRequestDto createQuizDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdQuiz = await _quizService.CreateQuizAsync(createQuizDto);
                
                return CreatedAtAction(nameof(GetQuiz), new { id = createdQuiz.Id }, createdQuiz);
            }
            catch (DbUpdateException ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data to the database.");
            }
            
        }
    }
}