using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Database4.Data;
using ConsoleDBTest.Models;

namespace ConsoleDBTest.Dealer {
    public class CountryDealer {
        public List<Country> Select(DbContext db) =>
           db.Database
             .SqlQuery<Country>($@"select * from {CountryDealer.tableName}").ToList();

        public List<Country> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<Country>($@"select * from {CountryDealer.tableName} where Id = '{id}'").ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {CountryDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {CountryDealer.tableName} {$"where Id = '{id}'"}");
        
        public int UpdateCountry(DbContext db, int id, string longName, string shortName, string isoCode, bool isActive) => 
            db.Database
                .ExecuteSqlCommand($@"update {CountryDealer.tableName} set LongName=N'{longName}', ShortName=N'{shortName}', ISOCode=N'{isoCode}', IsActive='{isActive}' {$"where Id = '{id}'"}");

        public int UpdateCountry(DbContext db, string longName, string shortName, string isoCode, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {CountryDealer.tableName} set LongName=N'{longName}', ShortName=N'{shortName}', ISOCode=N'{isoCode}', IsActive='{isActive}'");
        
        public int AddCountry(DbContext db, string longName, string shortName, string isoCode, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {CountryDealer.tableName} values (N'{longName}', N'{shortName}', N'{isoCode}', '{isActive}')");

        private const string tableName = nameof(AppDataContext.Countries);
    }
}
