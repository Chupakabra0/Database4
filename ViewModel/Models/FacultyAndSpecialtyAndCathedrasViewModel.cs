using System.Linq;
using ConsoleDBTest.Models;
using Database4.Data;

namespace ConsoleDBTest.ViewModels {
    public class FacultyAndSpecialtyAndCathedraViewModel {
        public FacultyAndSpecialtyAndCathedraViewModel(FacultyAndSpecialtyAndCathedra facultyAndSpecialtyAndCathedra, AppDataContext db) {
            this.Id                  = facultyAndSpecialtyAndCathedra.Id;
            this.FacultyAndSpecialty =
                FacultyAndSpecialtyAndCathedraViewModel
                    .GetFacultyAndSpecialtyName(facultyAndSpecialtyAndCathedra.FacultyAndSpecialtyId, db);
            this.Cathedra =
                FacultyAndSpecialtyAndCathedraViewModel.GetCathedraName(facultyAndSpecialtyAndCathedra.CathedraId, db);
            this.IsActive            = facultyAndSpecialtyAndCathedra.IsActive;
        }

        private static string GetFacultyAndSpecialtyName(int facultyAndSpecialtyId, AppDataContext db) {
            var facultyAndSpecialty = db
                                        ?.Database
                                        ?.SqlQuery<FacultyAndSpecialty>($"select * from {nameof(db.FacultyAndSpecialties)} where Id={facultyAndSpecialtyId}")
                                        ?.ToList()
                                        ?.First();

            if (facultyAndSpecialty is null) {
                return "null";
            }

            var faculty = db?.Database
                            ?.SqlQuery<Faculty>($"select * from {nameof(db.Faculties)} where Id={facultyAndSpecialty.FacultyId}")
                            ?.ToList()
                            ?.First();

            var specialty = db?.Database
                            .SqlQuery<Specialty>($"select * from {nameof(db.Specialties)} where Id={facultyAndSpecialty.SpecialtyId}")
                            ?.ToList()
                            ?.First();

            if (faculty is null || specialty is null) {
                return "null";
            }

            return $"{faculty.ShortName}-{specialty.Name}";
        }

        private static string GetCathedraName(int cathedraId, AppDataContext db) =>
            db?.Database?.SqlQuery<Cathedra>($"select * from {nameof(db.Cathedras)} where Id={cathedraId}")
              ?.ToList()
              ?.First()
              ?.ShortName ?? "null";

        public int    Id                  { get; set; }
        public string FacultyAndSpecialty { get; set; }
        public string Cathedra            { get; set; }
        public bool   IsActive            { get; set; }

        public override string ToString() => $"{this.FacultyAndSpecialty}-{this.Cathedra}";
    }
}
