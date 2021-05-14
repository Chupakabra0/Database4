using System.ComponentModel;
using ConsoleDBTest.Models;

namespace ConsoleDBTest.ViewModels {
    public class DegreeViewModel {
        public DegreeViewModel(Degree degree) {
            this.Id          = degree.Id;
            this.Name        = degree.Name;
            this.ShortLetter = degree.ShortLetter;
            this.IsActive    = degree.IsActive;
        }

        public int    Id          { get; set; }

        [DisplayName("Название")]
        public string Name        { get; set; }

        [DisplayName("Буква")]
        public string ShortLetter { get; set; }

        [DisplayName("Активность")]
        public bool   IsActive    { get; set; }

        public override string ToString() => this.Name;
    }
}
