using System;
using System.Collections.Generic;

#nullable disable

namespace Web_API.Models
{
    public partial class QuizQuestionsAnswer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? QuestionId { get; set; }
        public int? OrdinalNo { get; set; }

        public virtual QuizQuestion Question { get; set; }
    }
}
