using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class ClientCardDealer {
        public List<ClientCard> Select(DbContext db) =>
            db.Database
              .SqlQuery<ClientCard>($@"select * from {ClientCardDealer.tableName}")
              .ToList();

        public List<ClientCard> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<ClientCard>($@"select * from {ClientCardDealer.tableName} where Id = '{id}'")
              .ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {ClientCardDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {ClientCardDealer.tableName} where Id = '{id}'");

        public int UpdateCard(DbContext db, int id, DateTime? dateGiven, int? studentId, int? teacherId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {ClientCardDealer.tableName} set DateGiven={(dateGiven == null ? "null" : $"'{dateGiven.Value:yyyy/MM/dd}'")}, StudentId={(studentId == null ? "null" : studentId)}, TeacherId={(teacherId == null ? "null" : teacherId)}, IsActive='{isActive}' where Id = '{id}'");

        public int DisactiveCard(DbContext db, int id) =>
            db.Database
                .ExecuteSqlCommand($@"update {ClientCardDealer.tableName} set IsActive='false' where Id = '{id}'");

        public int UpdateCard(DbContext db, DateTime? dateGiven, int? studentId, int? teacherId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {ClientCardDealer.tableName} set DateGiven={(dateGiven == null ? "null" : $"'{dateGiven.Value:yyyy/MM/dd}'")}, StudentId={(studentId == null ? "null" : studentId)}, TeacherId={(teacherId == null ? "null" : teacherId)}, IsActive='{isActive}'");

        public int AddCard(DbContext db, DateTime? dateGiven, int? studentId, int? teacherId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {ClientCardDealer.tableName} (DateGiven, StudentId, TeacherId, IsActive) values ({(dateGiven == null ? "null" : $"'{dateGiven.Value:yyyy/MM/dd}'")}, {(studentId == null ? "null" : studentId)}, {(teacherId == null ? "null" : teacherId)}, '{isActive}')");
        
        private const string tableName = nameof(AppDataContext.ClientCards);
    }
}
