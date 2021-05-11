using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class GenreDealer {
        public List<Genre> Select(DbContext db) =>
            db.Database
              .SqlQuery<Genre>($@"select * from {GenreDealer.tableName}").ToList();

        public List<Genre> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<Genre>($@"select * from {GenreDealer.tableName} where Id = '{id}'").ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {GenreDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {GenreDealer.tableName} where Id = '{id}'");

        public int UpdateGenre(DbContext db, int id, string name, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {GenreDealer.tableName} set Name=N'{name}', IsActive='{isActive}' where Id = '{id}'");

        public int UpdateGenre(DbContext db, string name, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {GenreDealer.tableName} set Name=N'{name}', IsActive='{isActive}'");

        public int AddGenre(DbContext db, string name, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {GenreDealer.tableName} values (N'{name}', '{isActive}')");
        
        private const string tableName = nameof(AppDataContext.Genres);
    }
}
