using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class CityDealer {
        public List<City> Select(DbContext db) =>
            db.Database
              .SqlQuery<City>($@"select * from {CityDealer.tableName}")
              .ToList();

        public List<City> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<City>($@"select * from {CityDealer.tableName} where Id = '{id}'")
              .ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {CityDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {CityDealer.tableName} where Id = '{id}'");

        public int UpdateCity(DbContext db, int id, string name, int countryId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {CityDealer.tableName} set Name=N'{name}', CountryId={countryId}, IsActive='{isActive}' where Id = '{id}'");

        public int UpdateCity(DbContext db, string name, int countryId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {CityDealer.tableName} set Name=N'{name}', CountryId={countryId}, IsActive='{isActive}'");

        public int AddCity(DbContext db, string name, int countryId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {CityDealer.tableName} values (N'{name}', {countryId}, '{isActive}')");

        private const string tableName = nameof(AppDataContext.Cities);
    }
}
