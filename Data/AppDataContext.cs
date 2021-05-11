using System.Data.Entity;
using ConsoleDBTest.Models;

namespace Database4.Data {
    public class AppDataContext : DbContext {
        public AppDataContext() : this("UniversityLibraryDatabase"){

        }

        public AppDataContext(string nameOrConnectionString)
            : base(nameOrConnectionString) {
            this.Database.CreateIfNotExists();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Cathedra> Cathedras { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ClientCard> ClientCards { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<FacultyAndSpecialty> FacultyAndSpecialties { get; set; }
        public DbSet<FacultyAndSpecialtyAndCathedra> FacultyAndSpecialtyAndCathedras { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<LibraryTransaction> LibraryTransactions { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}
