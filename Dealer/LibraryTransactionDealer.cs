using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class LibraryTransactionDealer {
        public List<LibraryTransaction> Select(DbContext db) =>
            db.Database
              .SqlQuery<LibraryTransaction>($@"select * from {LibraryTransactionDealer.tableName}")
              .ToList();

        public List<LibraryTransaction> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<LibraryTransaction>($@"select * from {LibraryTransactionDealer.tableName} where Id = '{id}'")
              .ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {LibraryTransactionDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {LibraryTransactionDealer.tableName} where Id = '{id}'");

        public int UpdateTransaction(DbContext db, int id, DateTime? takeDate, DateTime? returnDate, int clientCardId, int workerId, int bookId, bool isReturnInTime, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {LibraryTransactionDealer.tableName} set TakeDate={(takeDate == null ? "null" : $"'{takeDate.Value:yyyy/MM/dd}'")}, ReturnDate={(returnDate == null ? "null" : $"'{returnDate.Value:yyyy/MM/dd}'")}, ClientCardId={clientCardId}, WorkerId={workerId}, BookId={bookId}, IsReturnInTime='{isReturnInTime}', IsActive='{isActive}' where Id = '{id}'");

        public int UpdateTransaction(DbContext db, DateTime? takeDate, DateTime? returnDate, int clientCardId, int workerId, int bookId, bool isReturnInTime, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {LibraryTransactionDealer.tableName} set TakeDate={(takeDate == null ? "null" : $"'{takeDate.Value:yyyy/MM/dd}'")}, ReturnDate={(returnDate == null ? "null" : $"'{returnDate.Value:yyyy/MM/dd}'")}, ClientCardId={clientCardId}, WorkerId={workerId}, BookId={bookId}, IsReturnInTime='{isReturnInTime}', IsActive='{isActive}'");
        
        public int DisactiveTransaction(DbContext db, int id) =>
            db.Database
                .ExecuteSqlCommand($@"update {LibraryTransactionDealer.tableName} set IsActive='{false}' where Id='{id}'");

        public int AddTransaction(DbContext db, DateTime? takeDate, DateTime? returnDate, int clientCardId, int workerId, int bookId, bool isReturnInTime, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {LibraryTransactionDealer.tableName} values ({(takeDate == null ? "null" : $"'{takeDate.Value:yyyy/MM/dd}'")}, {(returnDate == null ? "null" : $"'{returnDate.Value:yyyy/MM/dd}'")}, {clientCardId}, {workerId}, {bookId}, '{isReturnInTime}', '{isActive}')");

        private const string tableName = nameof(AppDataContext.LibraryTransactions);
    }
}
