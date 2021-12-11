using System;
using System.Collections.Generic;

#nullable disable

namespace Web_API.Models
{
    public partial class Semester
    {
        public Semester()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? DegreeId { get; set; }

        public virtual Degree Degree { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
