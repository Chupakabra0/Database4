using System.ComponentModel;
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

        [DisplayName("Полное название")]
        public string Name      { get; set; }

        [DisplayName("Краткое название")]
        public string ShortName { get; set; }
        
        [DisplayName("Активность")]
        public bool   IsActive  { get; set; }

        public override string ToString() => this.ShortName;
    }
}
