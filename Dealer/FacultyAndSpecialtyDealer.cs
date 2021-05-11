using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class FacultyAndSpecialtyDealer {
        public List<FacultyAndSpecialty> Select(DbContext db) =>
            db.Database
              .SqlQuery<FacultyAndSpecialty>($@"select * from {FacultyAndSpecialtyDealer.tableName}")
              .ToList();

        public List<FacultyAndSpecialty> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<FacultyAndSpecialty>($@"select * from {FacultyAndSpecialtyDealer.tableName} where Id = '{id}'")
              .ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {FacultyAndSpecialtyDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {FacultyAndSpecialtyDealer.tableName} where Id = '{id}'");

        public int UpdateFacultyAndSpecialty(DbContext db, int id, int facultyId, int specialtyId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {FacultyAndSpecialtyDealer.tableName} set FacultyId='{facultyId}', SpecialtyId={specialtyId}, IsActive='{isActive}' where Id = '{id}'");

        public int UpdateFacultyAndSpecialty(DbContext db, int facultyId, int specialtyId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {FacultyAndSpecialtyDealer.tableName} set FacultyId='{facultyId}', SpecialtyId={specialtyId}, IsActive='{isActive}'");

        public int AddFacultyAndSpecialty(DbContext db, int facultyId, int specialtyId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"insert into {FacultyAndSpecialtyDealer.tableName} values ('{facultyId}', {specialtyId}, '{isActive}')");

        private const string tableName = nameof(AppDataContext.FacultyAndSpecialties);
    }
}
