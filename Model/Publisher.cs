using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest.Models {
    public class Publisher {
        [Key]
        public int Id { get; set; }

        public string Name     { get; set; }
        public int    CityId   { get; set; }
        public bool   IsActive { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public virtual City City { get; set; }
    }
}
