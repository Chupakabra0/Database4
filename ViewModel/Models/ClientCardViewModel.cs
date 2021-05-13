using System;
using System.Linq;
using ConsoleDBTest.Models;
using ConsoleDBTest.Utils.StringUtils;
using Database4.Data;

namespace ConsoleDBTest.ViewModels {
    public class ClientCardViewModel {
        public ClientCardViewModel(ClientCard clientCard, AppDataContext db) {
            this.Id        = clientCard.Id;
            this.DateGiven = ClientCardViewModel.GetDate(clientCard.DateGiven);
            this.Student   = ClientCardViewModel.GetStudentName(clientCard.StudentId, db);
            this.Teacher   = ClientCardViewModel.GetTeacherName(clientCard.TeacherId, db);
            this.IsActive  = clientCard.IsActive;
        }

        private static string GetDate(DateTime? dateTime) =>
            dateTime?.ToShortDateString() ?? "null";

        private static string GetStudentName(int? studentId, AppDataContext db) {
            if (studentId == null) {
                return "null";
            }

            var student = db
                          ?.Database?.SqlQuery<Student>($"select * from {nameof(db.Students)} where Id={studentId}")
                          ?.ToList()
                          ?.First();

            return student == null ? "null" : StringUtils.GetPersonName(student.Name, student.Surname, student.Patronymic);
        }

        private static string GetTeacherName(int? teacherId, AppDataContext db) {
            if (teacherId == null) {
                return "null";
            }

            var teacher = db?.Database?.SqlQuery<Teacher>($"select * from {nameof(db.Teachers)} where Id={teacherId}")
                            ?.ToList()
                            ?.First();

            return teacher == null ? "null" : StringUtils.GetPersonName(teacher.Name, teacher.Surname, teacher.Patronymic);
        }

        public int    Id        { get; set; }
        public string DateGiven { get; set; }
        public string Student   { get; set; }
        public string Teacher   { get; set; }
        public bool   IsActive  { get; set; }

        public override string ToString() {
            var temp = this.Student == "null" ? this.Teacher : this.Student;
            return $"#{this.Id}-{temp}";
        }
    }
}
