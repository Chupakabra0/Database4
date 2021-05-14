using System.ComponentModel;
using ConsoleDBTest.Models;

namespace ConsoleDBTest.ViewModels {
    public class FacultyViewModel {
        public FacultyViewModel(Faculty faculty) {
            this.Id          = faculty.Id;
            this.Name        = faculty.Name;
            this.ShortName   = faculty.ShortName;
            this.ShortLetter = faculty.ShortLetter;
            this.IsActive    = faculty.IsActive;
        }

        public int    Id          { get; set; }

        [DisplayName("Полное название")]
        public string Name        { get; set; }

        [DisplayName("Короткое название")]
        public string ShortName   { get; set; }

        [DisplayName("Буква")]
        public string ShortLetter { get; set; }

        [DisplayName("Активность")]
        public bool   IsActive    { get; set; }

        public override string ToString() => this.ShortName;
    }
}
