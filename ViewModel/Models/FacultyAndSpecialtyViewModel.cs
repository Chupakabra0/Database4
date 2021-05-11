using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.ViewModels {
    public class FacultyAndSpecialtyViewModel {
        public FacultyAndSpecialtyViewModel(FacultyAndSpecialty facultyAndSpecialty, AppDataContext db) {
            this.Id        = facultyAndSpecialty.Id;
            this.Faculty   = FacultyAndSpecialtyViewModel.GetFacultyName(facultyAndSpecialty.FacultyId, db);
            this.Specialty = FacultyAndSpecialtyViewModel.GetSpecialtyName(facultyAndSpecialty.SpecialtyId, db);
            this.IsActive  = facultyAndSpecialty.IsActive;
        }

        private static string GetFacultyName(int facultyId, AppDataContext db) =>
            db?.Database
              ?.SqlQuery<Faculty>($"select * from {nameof(db.Faculties)} where Id={facultyId}")
              ?.ToList()
              ?.First()
              ?.ShortName ?? "null";

        private static string GetSpecialtyName(int specialtyId, AppDataContext db) =>
            db?.Database
              ?.SqlQuery<Specialty>($"select * from {nameof(db.Specialties)} where Id={specialtyId}")
              ?.ToList()
              ?.First()
              ?.Name ?? "null";

        public int    Id        { get; set; }
        public string Faculty   { get; set; }
        public string Specialty { get; set; }
        public bool   IsActive  { get; set; }

        public override string ToString() => $"{this.Faculty}-{this.Specialty}";
    }
}
