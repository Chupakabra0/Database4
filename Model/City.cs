using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest.Models {
    public class City {
        [Key]
        public int Id { get; set; }

        public string Name      { get; set; }
        public int    CountryId { get; set; }
        public bool   IsActive  { get; set; }

        public virtual ICollection<Publisher> Publishers { get; set; }
        public virtual ICollection<Teacher>   Teachers   { get; set; }
        public virtual ICollection<Student>   Students   { get; set; }

        public virtual Country Country { get; set; }
    }
}
