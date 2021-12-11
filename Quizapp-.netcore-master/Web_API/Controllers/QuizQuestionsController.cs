using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API.Models;
using System.Net.Http;
using System.Net;



namespace Web_API.Controllers
 
{
    public class QuizQuestionsController : ControllerBase
    {

        private readonly Quiz_DBContext _context;
        public QuizQuestionsController(Quiz_DBContext dBContext)
        {
            _context = dBContext;
        }

        [HttpGet]
        [Route("api/Questions")]
        public IActionResult GetQuestions()
        {        
                var Qns = _context.QuizQuestions
                    .Select(x => new { Id = x.Id, Name = x.Name, x.Option1, x.Option2, x.Option3, x.Option4 })
                    .OrderBy(y => Guid.NewGuid())
                    .Take(10)
                    .ToArray();
                var updated = Qns.AsEnumerable()
                    .Select(x => new
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                    }).ToList();
            return Ok(updated);         
        }

        [HttpPost]
        [Route("api/Answers")]
        public IActionResult GetAnswers(int[] qIDs)
        {
                var result = _context.QuizQuestions
                     .AsEnumerable()
                     .Where(y => qIDs.Contains(y.Id))
                     .OrderBy(x => { return Array.IndexOf(qIDs, x.Id); })
                     .Select(z => z.Answer)
                     .ToArray();
                return Ok(result);         
        }
    }
}
