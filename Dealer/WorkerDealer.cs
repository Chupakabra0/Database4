using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class WorkerDealer {
        public List<Worker> Select(DbContext db) =>
            db.Database
              .SqlQuery<Worker>($@"select * from {WorkerDealer.tableName}").ToList();

        public List<Worker> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<Worker>($@"select * from {WorkerDealer.tableName} where Id = '{id}'").ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {WorkerDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {WorkerDealer.tableName} where Id = '{id}'");

        public int UpdateWorker(DbContext db, int id, string name, string surname, string patronymic, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {WorkerDealer.tableName} set Name=N'{name}', Surname=N'{surname}', Patronymic=N'{patronymic}', IsActive='{isActive}' where Id = '{id}'");

        public int UpdateWorker(DbContext db, string name, string surname, string patronymic, bool isActive) =>
            db.Database.
               ExecuteSqlCommand($@"update{WorkerDealer.tableName} set Name = N'{name}', Surname = N'{surname}', Patronymic = N'{patronymic}', IsActive = '{isActive}'");

        public int AddWorker(DbContext db, string name, string surname, string patronymic, bool isActive) =>
            db.Database.
               ExecuteSqlCommand($@"insert into {WorkerDealer.tableName} values (N'{name}', N'{surname}',N'{patronymic}', '{isActive}')");

        private const string tableName = nameof(AppDataContext.Workers);
    }
}
