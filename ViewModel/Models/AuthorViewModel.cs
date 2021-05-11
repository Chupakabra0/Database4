using ConsoleDBTest.Models;
using System.Linq;
using System;
using Database4.Data;

namespace ConsoleDBTest.ViewModels {
    public class AuthorViewModel {
        public AuthorViewModel(Author author) {
            this.Id         = author.Id;
            this.Name       = author.Name;
            this.Surname    = author.Surname;
            this.Patronymic = author.Patronymic;
            this.Pseudonym  = author.Pseudonym;
            this.IsActive   = author.IsActive;
        }

        public int    Id         { get; set; }
        public string Name       { get; set; }
        public string Surname    { get; set; }
        public string Patronymic { get; set; }
        public string Pseudonym  { get; set; }
        public bool   IsActive   { get; set; }

        public override string ToString() {
            return "FUCK";
            //return string.IsNullOrEmpty(this.Pseudonym)
            //    ? StringUtils.GetPersonName(this.Name, this.Surname, this.Patronymic)
            //    : this.Pseudonym;
        }
    }
}
