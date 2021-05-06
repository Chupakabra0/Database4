using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest.Models {
    public class FacultyAndSpecialtyAndCathedra {
        [Key]
        public int Id { get; set; }

        public int  FacultyAndSpecialtyId { get; set; }
        public int  CathedraId            { get; set; }
        public bool IsActive              { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual FacultyAndSpecialty FacultyAndSpecialty { get; set; }
        public virtual Cathedra            Cathedra            { get; set; }
    }

}
