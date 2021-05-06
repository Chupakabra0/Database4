using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest.Models {
    public class Country {
        [Key]
        public int Id { get; set; }

        public string LongName   { get; set; }
        public string ShortName  { get; set; }
        public string ISOCode    { get; set; }
        public bool   IsActive   { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
