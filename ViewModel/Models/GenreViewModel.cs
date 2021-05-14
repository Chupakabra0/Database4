using System.ComponentModel;
using ConsoleDBTest.Models;

namespace ConsoleDBTest.ViewModels {
    public class GenreViewModel {
        public GenreViewModel(Genre genre) {
            this.Id       = genre.Id;
            this.Name     = genre.Name;
            this.IsActive = genre.IsActive;
        }

        public int    Id       { get; set; }

        [DisplayName("Название")]
        public string Name     { get; set; }

        [DisplayName("Активность")]
        public bool   IsActive { get; set; }

        public override string ToString() => this.Name;
    }
}
