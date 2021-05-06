using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleDBTest.Models {
    public class Teacher {
        [Key]
        public int Id { get; set; }

        public string Name       { get; set; }
        public string Surname    { get; set; }
        public string Patronymic { get; set; }
        public int    CityId     { get; set; }
        public int    CathedraId { get; set; }
        public bool   IsActive   { get; set; }

        public virtual ICollection<ClientCard> ClientCards { get; set; }

        public virtual City     City     { get; set; }
        public virtual Cathedra Cathedra { get; set; }
    }
}
