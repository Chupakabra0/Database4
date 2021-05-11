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
        public string Name        { get; set; }
        public string ShortLetter { get; set; }
        public bool   IsActive    { get; set; }

        public override string ToString() {
            return this.Name;
        }
    }
}
