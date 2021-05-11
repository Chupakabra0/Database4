using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class SpecialtyDealer {
        public List<Specialty> Select(DbContext db) =>
            db.Database
              .SqlQuery<Specialty>($@"select * from {SpecialtyDealer.tableName}").ToList();

        public List<Specialty> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<Specialty>($@"select * from {SpecialtyDealer.tableName} where Id = '{id}'").ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {SpecialtyDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {SpecialtyDealer.tableName} where Id = '{id}'");

        public int UpdateSpecialty(DbContext db, int id, string name, string shortLetter, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {SpecialtyDealer.tableName} set Name=N'{name}', ShortLetter=N'{shortLetter}', IsActive='{isActive}' where Id = '{id}'");

        public int UpdateSpecialty(DbContext db, string name, string shortLetter, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {SpecialtyDealer.tableName} set Name=N'{name}', ShortLetter=N'{shortLetter}', IsActive='{isActive}'");

        public int AddSpecialty(DbContext db, string name, string shortLetter, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {SpecialtyDealer.tableName} values (N'{name}', N'{shortLetter}','{isActive}')");

        private const string tableName = nameof(AppDataContext.Specialties);
    }
}