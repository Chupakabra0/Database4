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
        public string Name        { get; set; }
        public string ShortName   { get; set; }
        public string ShortLetter { get; set; }
        public bool   IsActive    { get; set; }

        public override string ToString() => this.Name;
    }
}
