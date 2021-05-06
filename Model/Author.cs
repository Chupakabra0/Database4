using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest.Models {
    public class Author {
        [Key]
        public int Id { get; set; }

        public string Name       { get; set; }
        public string Surname    { get; set; }
        public string Patronymic { get; set; }
        public string Pseudonym  { get; set; }
        public bool   IsActive   { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
