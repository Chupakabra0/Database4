using System;
using System.ComponentModel;
using System.Linq;
using ConsoleDBTest.Models;
using ConsoleDBTest.Utils.StringUtils;
using Database4.Data;

namespace ConsoleDBTest.ViewModels {
    public class LibraryTransactionViewModel {
        public LibraryTransactionViewModel(LibraryTransaction libraryTransaction, AppDataContext db) {
            this.Id             = libraryTransaction.Id;
            this.TakeDate       = LibraryTransactionViewModel.GetDate(libraryTransaction.TakeDate);
            this.ReturnDate     = LibraryTransactionViewModel.GetDate(libraryTransaction.ReturnDate);
            this.ClientCard     = LibraryTransactionViewModel.GetCard(libraryTransaction.ClientCardId);
            this.Worker         = LibraryTransactionViewModel.GetWorkerName(libraryTransaction.WorkerId, db);
            this.Book           = LibraryTransactionViewModel.GetBookName(libraryTransaction.BookId, db);
            this.IsReturnInTime = libraryTransaction.IsReturnInTime;
            this.IsActive       = libraryTransaction.IsActive;
        }

        private static string GetDate(DateTime? dateTime) =>
            dateTime?.ToShortDateString() ?? "null";

        private static string GetCard(int clientCardId) =>
            $"№{clientCardId}";

        private static string GetWorkerName(int workerId, AppDataContext db) {
            var worker = db?.Database?.SqlQuery<Worker>($"select * from {nameof(db.Workers)} where Id={workerId}")
                           ?.ToList()
                           ?.First();

            return worker == null ? "null" : StringUtils.GetPersonName(worker.Name, worker.Surname, worker.Patronymic);
        }

        private static string GetBookName(int bookId, AppDataContext db) =>
            db?.Database?.SqlQuery<Book>($"select * from {nameof(db.Books)} where Id={bookId}")
              ?.ToList()
              ?.First()
              ?.Name ?? "null";

        public int    Id             { get; set; }

        [DisplayName("Дата взятия")]
        public string TakeDate       { get; set; }

        [DisplayName("Дата возвращения")]
        public string ReturnDate     { get; set; }

        [DisplayName("Карта")]
        public string ClientCard     { get; set; }

        [DisplayName("Выдал")]
        public string Worker         { get; set; }

        [DisplayName("Книга")]
        public string Book           { get; set; }

        [DisplayName("Обязан возвращать вовремя")]
        public bool   IsReturnInTime { get; set; }

        [DisplayName("Активность")]
        public bool   IsActive       { get; set; }

        public override string ToString() => $"#{this.Id}-{this.Book}-\"{this.ClientCard}\"";
    }
}
