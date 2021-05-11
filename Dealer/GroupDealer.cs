using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class GroupDealer {
        public List<Group> Select(DbContext db) =>
            db.Database
              .SqlQuery<Group>($@"select * from {GroupDealer.tableName}")
              .ToList();

        public List<Group> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<Group>($@"select * from {GroupDealer.tableName} where Id = '{id}'")
              .ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {GroupDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {GroupDealer.tableName} where Id = '{id}'");

        public int UpdateGroup(DbContext db, int id, int facultyAndSpecialtyAndCathedraId, int degreeId, int year, int serial, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {GroupDealer.tableName} set FacultyAndSpecialtyAndCathedraId='{facultyAndSpecialtyAndCathedraId}', DegreeId={degreeId}, Year={year}, Serial={serial}, IsActive='{isActive}' where Id = '{id}'");

        public int UpdateGroup(DbContext db, int facultyAndSpecialtyAndCathedraId, int degreeId, int year, int serial, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {GroupDealer.tableName} set FacultyAndSpecialtyAndCathedraId='{facultyAndSpecialtyAndCathedraId}', DegreeId={degreeId}, Year={year}, Serial={serial}, IsActive='{isActive}'");

        public int AddGroup(DbContext db, int facultyAndSpecialtyAndCathedraId, int degreeId, int year, int serial, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {GroupDealer.tableName} values ({year}, {serial}, {facultyAndSpecialtyAndCathedraId}, {degreeId}, '{isActive}')");

        private const string tableName = nameof(AppDataContext.Groups);
    }
}
