using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class BookDealer {
        public List<Book> Select(DbContext db) =>
            db.Database
              .SqlQuery<Book>($@"select * from {BookDealer.tableName}")
              .ToList();

        public List<Book> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<Book>($@"select * from {BookDealer.tableName} where Id = '{id}'")
              .ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {BookDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {BookDealer.tableName} where Id = '{id}'");

        public int UpdateBook(DbContext db, int id, string name, int authorId, int publisherId, int genreId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {BookDealer.tableName} set Name=N'{name}', AuthorId={authorId}, PublisherId={publisherId}, GenreId={genreId}, IsActive='{isActive}' where Id = '{id}'");

        public int UpdateBook(DbContext db, string name, int authorId, int publisherId, int genreId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {BookDealer.tableName} set Name=N'{name}', AuthorId={authorId}, PublisherId={publisherId}, GenreId={genreId}, IsActive='{isActive}'");

        public int AddBook(DbContext db, string name, int authorId, int publisherId, int genreId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {BookDealer.tableName} values (N'{name}', {authorId}, {publisherId}, {genreId}, '{isActive}')");

        private const string tableName = nameof(AppDataContext.Books);
    }
}
