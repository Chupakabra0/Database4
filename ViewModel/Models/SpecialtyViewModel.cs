using ConsoleDBTest.Models;

namespace ConsoleDBTest.ViewModels {
    public class SpecialtyViewModel {
        public SpecialtyViewModel(Specialty specialty) {
            this.Id          = specialty.Id;
            this.Name        = specialty.Name;
            this.ShortLetter = specialty.ShortLetter;
            this.IsActive    = specialty.IsActive;
        }

        public int    Id          { get; set; }
        public string Name        { get; set; }
        public string ShortLetter { get; set; }
        public bool   IsActive    { get; set; }

        public override string ToString() => this.Name;
    }
}
