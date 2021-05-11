using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class FacultyDealer {
        public List<Faculty> Select(DbContext db) =>
            db.Database
              .SqlQuery<Faculty>($@"select * from {FacultyDealer.tableName}")
              .ToList();

        public List<Faculty> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<Faculty>($@"select * from {FacultyDealer.tableName} where Id = '{id}'")
              .ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {FacultyDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {FacultyDealer.tableName} where Id = '{id}'");

        public int UpdateFaculty(DbContext db, int id, string name, string shortName, string shortLetter, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {FacultyDealer.tableName} set Name=N'{name}', ShortName=N'{shortName}', ShortLetter=N'{shortLetter}', IsActive='{isActive}' where Id = '{id}'");

        public int UpdateFaculty(DbContext db, string name, string shortName, string shortLetter, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {FacultyDealer.tableName} set Name=N'{name}', ShortName=N'{shortName}', ShortLetter=N'{shortLetter}', IsActive='{isActive}'");

        public int AddFaculty(DbContext db, string name, string shortName, string shortLetter, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {FacultyDealer.tableName} values (N'{name}', N'{shortName}', N'{shortLetter}', '{isActive}')");

        private const string tableName = nameof(AppDataContext.Faculties);
    }
}
