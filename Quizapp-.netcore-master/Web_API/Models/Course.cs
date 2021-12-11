using System;
using System.Collections.Generic;

#nullable disable

namespace Web_API.Models
{
    public partial class Course
    {
        public Course()
        {
            Quizzes = new HashSet<Quiz>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SemesterId { get; set; }

        public virtual Semester Semester { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
