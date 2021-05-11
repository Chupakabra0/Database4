using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class DegreeDealer {
        public List<Degree> Select(DbContext db) =>
            db.Database
              .SqlQuery<Degree>($@"select * from {DegreeDealer.tableName}").ToList();

        public List<Degree> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<Degree>($@"select * from {DegreeDealer.tableName} where Id = '{id}'").ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {DegreeDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {DegreeDealer.tableName} where Id = '{id}'");

        public int UpdateDegree(DbContext db, int id, string name, string shortLetter, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {DegreeDealer.tableName} set Name=N'{name}', ShortLetter=N'{shortLetter}', IsActive='{isActive}' where Id = '{id}'");

        public int UpdateDegree(DbContext db, string name, string shortLetter, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {DegreeDealer.tableName} set Name=N'{name}', ShortLetter=N'{shortLetter}', IsActive='{isActive}'");

        public int AddDegree(DbContext db, string name, string shortLetter, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {DegreeDealer.tableName} values (N'{name}', N'{shortLetter}','{isActive}')");

        private const string tableName = nameof(AppDataContext.Degrees);
    }
}
