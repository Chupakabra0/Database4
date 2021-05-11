using ConsoleDBTest.Models;

namespace ConsoleDBTest.ViewModels {
    public class CathedraViewModel {
        public CathedraViewModel(Cathedra cathedra) {
            this.Id        = cathedra.Id;
            this.Name      = cathedra.Name;
            this.ShortName = cathedra.ShortName;
            this.IsActive  = cathedra.IsActive;
        }

        public int    Id        { get; set; }
        public string Name      { get; set; }
        public string ShortName { get; set; }
        public bool   IsActive  { get; set; }

        public override string ToString() {
            return this.Name;
        }
    }
}
