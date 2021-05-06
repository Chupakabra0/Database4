using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest.Models {
    public class Book {
        [Key]
        public int Id { get; set; }

        public string Name        { get; set; }
        public int    AuthorId    { get; set; }
        public int    PublisherId { get; set; }
        public int    GenreId     { get; set; }
        public bool   IsActive    { get; set; }

        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }

        public virtual Author    Author    { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual Genre     Genre     { get; set; }
    }
}
