using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.Dealer {
    public class StudentDealer {
        public List<Student> Select(DbContext db) =>
            db.Database
              .SqlQuery<Student>($@"select * from {StudentDealer.tableName}").ToList();

        public List<Student> Select(DbContext db, int id) =>
            db.Database
              .SqlQuery<Student>($@"select * from {StudentDealer.tableName} where Id = '{id}'").ToList();

        public int Delete(DbContext db) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {StudentDealer.tableName}");

        public int Delete(DbContext db, int id) =>
            db.Database
              .ExecuteSqlCommand($@"delete from {StudentDealer.tableName} where Id = '{id}'");

        public int UpdateStudent(DbContext db, int id, string name, string surname, string patronymic, int cityId, int groupId, bool isActive) =>
            db.Database
              .ExecuteSqlCommand($@"update {StudentDealer.tableName} set Name=N'{name}', Surname=N'{surname}', Patronymic=N'{patronymic}', CityId={cityId}, GroupId={groupId}, IsActive='{isActive}' where Id = '{id}'");

        public int UpdateStudent(DbContext db, string name, string surname, string patronymic, int cityId, int groupId, bool isActive) =>
            db.Database.
               ExecuteSqlCommand($@"update{StudentDealer.tableName} set Name = N'{name}', Surname = N'{surname}', Patronymic = N'{patronymic}', CityId={cityId}, GroupId={groupId}, IsActive = '{isActive}'");

        public int AddStudent(DbContext db, string name, string surname, string patronymic, int cityId, int groupId, bool isActive) =>
            db.Database.
               ExecuteSqlCommand($@"insert into {StudentDealer.tableName} values (N'{name}', N'{surname}',N'{patronymic}', {cityId}, {groupId}, '{isActive}')");

        private const string tableName = nameof(AppDataContext.Students);
    }
}
