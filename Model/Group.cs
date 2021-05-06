using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest.Models {
    public class Group {
        [Key]
        public int Id { get; set; }

        public int  Year                             { get; set; }
        public int  Serial                           { get; set; }
        public int  FacultyAndSpecialtyAndCathedraId { get; set; }
        public int  DegreeId                         { get; set; }
        public bool IsActive                         { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual FacultyAndSpecialtyAndCathedra FacultyAndSpecialtyAndCathedra { get; set; }
        public virtual Degree                         Degree                         { get; set; }
    }
}
