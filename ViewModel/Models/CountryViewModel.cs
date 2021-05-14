using System.ComponentModel;
using ConsoleDBTest.Models;

namespace ConsoleDBTest.ViewModels {
    public class CountryViewModel {
        public CountryViewModel(Country country) {
            this.Id        = country.Id;
            this.LongName  = country.LongName;
            this.ShortName = country.ShortName;
            this.ISOCode   = country.ISOCode;
            this.IsActive  = country.IsActive;
        }

        public int    Id        { get; set; }
        
        [DisplayName("Полное название")]
        public string LongName  { get; set; }

        [DisplayName("Короткое название")]
        public string ShortName { get; set; }

        [DisplayName("Код ISO")]
        public string ISOCode   { get; set; }

        [DisplayName("Активность")]
        public bool   IsActive  { get; set; }

        public override string ToString() => this.ShortName;   
    }
}
