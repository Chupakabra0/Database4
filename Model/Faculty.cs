using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest.Models {
    public class Faculty {
        [Key]
        public int Id { get; set; }

        public string Name        { get; set; }
        public string ShortName   { get; set; }
        public string ShortLetter { get; set; }
        public bool   IsActive    { get; set; }

        public virtual ICollection<FacultyAndSpecialty> FacultiesAndSpecialties { get; set; }
    }
}
