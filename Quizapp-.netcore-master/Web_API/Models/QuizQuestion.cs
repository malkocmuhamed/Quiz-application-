using System;
using System.Collections.Generic;

#nullable disable

namespace Web_API.Models
{
    public partial class QuizQuestion
    {
        public QuizQuestion()
        {
            QuizQuestionsAnswers = new HashSet<QuizQuestionsAnswer>();
        }

        public int Id { get; set; }
        //public int? RightAnswer { get; set; }

        public Nullable<int> Answer { get; set; }
        public string Name { get; set; }
        public int? QuizId { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }

        public virtual Quiz Quiz { get; set; }


        public virtual ICollection<QuizQuestionsAnswer> QuizQuestionsAnswers { get; set; }
    }
}
