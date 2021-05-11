using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class CathedraDealer {
        public List<Cathedra> Select(DbContext db) =>
            db.Database
              .SqlQuery<Cathedra>($@"select * from {CathedraDealer.tableName}")
              .ToList();

        public List<Cathedra> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<Cathedra>($@"select * from {CathedraDealer.tableName} where Id = '{id}'")
              .ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {CathedraDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {CathedraDealer.tableName} where Id = '{id}'");

        public int UpdateCathedra(DbContext db, int id, string name, string shortName, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {CathedraDealer.tableName} set Name=N'{name}', ShortName=N'{shortName}', IsActive='{isActive}' where Id = '{id}'");

        public int UpdateCathedra(DbContext db, string name, string shortName, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {CathedraDealer.tableName} set Name=N'{name}', ShortName=N'{shortName}', IsActive='{isActive}'");

        public int AddCathedra(DbContext db, string name, string shortName, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {CathedraDealer.tableName} values (N'{name}', N'{shortName}', '{isActive}')");

        private const string tableName = nameof(AppDataContext.Cathedras);
    }
}