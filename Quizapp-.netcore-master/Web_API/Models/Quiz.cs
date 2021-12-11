using System;
using System.Collections.Generic;

#nullable disable

namespace Web_API.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            QuizQuestions = new HashSet<QuizQuestion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
