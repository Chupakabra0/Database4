using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest.Models {
    public class Degree {
        [Key]
        public int Id { get; set; }

        public string Name        { get; set; }
        public string ShortLetter { get; set; }
        public bool   IsActive    { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
