using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_API.Models;
using Web_API.Helpers;
using Web_API.Entities;
using Microsoft.Extensions.Options;

namespace Web_API.Services
{

    public interface ICourseService
    {
        IEnumerable<Course> GetCourses();
    }
    public class CourseService: ICourseService
    {
        private readonly Quiz_DBContext _context;

        public CourseService(Quiz_DBContext context)
        {
            this._context = context;
        }

        public IEnumerable<Course> GetCourses()
        {
            return _context.Courses;
        }

    }
}
