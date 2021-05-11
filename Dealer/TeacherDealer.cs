using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class TeacherDealer {
        public List<Teacher> Select(DbContext db) =>
            db.Database
              .SqlQuery<Teacher>($@"select * from {TeacherDealer.tableName}").ToList();

        public List<Teacher> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<Teacher>($@"select * from {TeacherDealer.tableName} where Id = '{id}'").ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {TeacherDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {TeacherDealer.tableName} where Id = '{id}'");

        public int UpdateTeacher(DbContext db, int id, string name, string surname, string patronymic, int cityId, int cathedraId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {TeacherDealer.tableName} set Name=N'{name}', Surname=N'{surname}', Patronymic=N'{patronymic}', CityId={cityId}, CathedraId={cathedraId}, IsActive='{isActive}' where Id = '{id}'");

        public int UpdateTeacher(DbContext db, string name, string surname, string patronymic, int cityId, int cathedraId, bool isActive) =>
            db.Database.
               ExecuteSqlCommand($@"update{TeacherDealer.tableName} set Name = N'{name}', Surname = N'{surname}', Patronymic = N'{patronymic}', CityId={cityId}, CathedraId={cathedraId}, IsActive = '{isActive}'");

        public int AddTeacher(DbContext db, string name, string surname, string patronymic, int cityId, int cathedraId, bool isActive) =>
            db.Database.
               ExecuteSqlCommand($@"insert into {TeacherDealer.tableName} values (N'{name}', N'{surname}',N'{patronymic}', {cityId}, {cathedraId}, '{isActive}')");

        private const string tableName = nameof(AppDataContext.Teachers);
    }
}
