using ConsoleDBTest.Models;
using System.Linq;
using System;
using Database4.Data;
using ConsoleDBTest.Utils.StringUtils;
using System.ComponentModel;

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

        [DisplayName("Имя")]
        public string Name       { get; set; }
        
        [DisplayName("Фамилия")]
        public string Surname    { get; set; }

        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        [DisplayName("Псевдоним")]
        public string Pseudonym  { get; set; }

        [DisplayName("Активность")]
        public bool   IsActive   { get; set; }

        public override string ToString() {
            return string.IsNullOrEmpty(this.Pseudonym)
                ? StringUtils.GetPersonName(this.Name, this.Surname, this.Patronymic)
                : this.Pseudonym;
        }
    }
}
