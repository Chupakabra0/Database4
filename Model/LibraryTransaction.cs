using System;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest.Models {
    public class LibraryTransaction {
        [Key]
        public int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? TakeDate   { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ReturnDate { get; set; }
        public int  ClientCardId    { get;    set; }
        public int  WorkerId        { get;    set; }
        public int  BookId          { get;    set; }
        public bool IsReturnInTime  { get;    set; }
        public bool IsActive        { get;    set; }

        public virtual ClientCard ClientCard { get; set; }
        public virtual Worker     Worker     { get; set; }
        public virtual Book       Book       { get; set; }
    }
}
