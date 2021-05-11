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
        public string LongName  { get; set; }
        public string ShortName { get; set; }
        public string ISOCode   { get; set; }
        public bool   IsActive  { get; set; }

        public override string ToString() => this.ShortName;   
    }
}
