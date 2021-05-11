using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class FacultyAndSpecialtyAndCathedraDealer {
        public List<FacultyAndSpecialtyAndCathedra> Select(DbContext db) =>
            db.Database
              .SqlQuery<FacultyAndSpecialtyAndCathedra>($@"select * from {FacultyAndSpecialtyAndCathedraDealer.tableName}")
              .ToList();

        public List<FacultyAndSpecialtyAndCathedra> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<FacultyAndSpecialtyAndCathedra>($@"select * from {FacultyAndSpecialtyAndCathedraDealer.tableName} where Id = '{id}'")
              .ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {FacultyAndSpecialtyAndCathedraDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {FacultyAndSpecialtyAndCathedraDealer.tableName} where Id = '{id}'");

        public int UpdateFacultyAndSpecialtyAndCathedra(DbContext db, int id, int facultyAndSpecialtyId, int cathedraId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {FacultyAndSpecialtyAndCathedraDealer.tableName} set FacultyAndSpecialtyId='{facultyAndSpecialtyId}', CathedraId={cathedraId}, IsActive='{isActive}' where Id = '{id}'");

        public int UpdateFacultyAndSpecialtyAndCathedra(DbContext db, int facultyAndSpecialtyId, int cathedraId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {FacultyAndSpecialtyAndCathedraDealer.tableName} set FacultyAndSpecialtyId='{facultyAndSpecialtyId}', CathedraId={cathedraId}, IsActive='{isActive}'");

        public int AddFacultyAndSpecialtyAndCathedra(DbContext db, int facultyAndSpecialtyId, int cathedraId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {FacultyAndSpecialtyAndCathedraDealer.tableName} values ('{facultyAndSpecialtyId}', {cathedraId}, '{isActive}')");

        private const string tableName = nameof(AppDataContext.FacultyAndSpecialtyAndCathedras);
    }
}
