using System;
using System.Collections.Generic;

#nullable disable

namespace Web_API.Models
{
    public partial class Degree
    {
        public Degree()
        {
            Semesters = new HashSet<Semester>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
    }
}
