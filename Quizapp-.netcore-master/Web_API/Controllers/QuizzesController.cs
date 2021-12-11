using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API.Helpers;
using Web_API.Models;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {

        private readonly Quiz_DBContext _context;

        public QuizzesController(Quiz_DBContext context)
        {
            _context = context;
           
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuizbyID(int id)
        {   
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return quiz;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var quizzes = _context.Quizzes;
            return Ok(quizzes);
        }

        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            try
            {
                if (quiz == null)
                {
                    return BadRequest();
                }

                _context.Quizzes.Add(quiz);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetQuizbyID", new { id = quiz.Id }, quiz);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }
    }
}
