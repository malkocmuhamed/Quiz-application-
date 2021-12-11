using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API.Helpers;
using Web_API.Models;
using Web_API.Services;
using Microsoft.Extensions.Options;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        private readonly Quiz_DBContext _context;

        public CoursesController(Quiz_DBContext context, ICourseService courseService)
        {
            _context = context;
            _courseService = courseService;

        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = _courseService.GetCourses();
            return Ok(courses);
        }

    }
}

