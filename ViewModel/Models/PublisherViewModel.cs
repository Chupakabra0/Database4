using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.ViewModels {
    public class PublisherViewModel {
        public PublisherViewModel(Publisher publisher, AppDataContext db = null) {
            this.Id       = publisher.Id;
            this.Name     = publisher.Name;
            this.City     = PublisherViewModel.GetCityName(publisher.CityId, db);
            this.IsActive = publisher.IsActive;
        }

        private static string GetCityName(int cityId, AppDataContext db) =>
            db?.Database
              ?.SqlQuery<City>($"select * from {nameof(db.Cities)} where Id={cityId}")
              ?.ToList()
              ?.First().Name ?? "null";

        public int    Id       { get; set; }
        public string Name     { get; set; }
        public string City     { get; set; }
        public bool   IsActive { get; set; }

        public override string ToString() => $"\"{this.Name}\"";
    }
}
