using System.ComponentModel;
using System.Linq;
using ConsoleDBTest.Models;
using ConsoleDBTest.Utils.StringUtils;
using Database4.Data;

namespace ConsoleDBTest.ViewModels {
    public class BookViewModel {
        public BookViewModel(Book book, AppDataContext db = null) {
            this.Id        = book.Id;
            this.Name      = book.Name;
            this.Author    = BookViewModel.GetAuthorName(book.AuthorId, db);
            this.Publisher = BookViewModel.GetPublisherName(book.PublisherId, db);
            this.Genre     = BookViewModel.GetGenreName(book.GenreId, db);
            this.IsActive  = book.IsActive;
        }

        private static string GetAuthorName(int authorId, AppDataContext db) {
            var author = db?.Database?.SqlQuery<Author>($"select * from {nameof(db.Authors)} where Id={authorId}")
                           ?.ToList()
                           ?.First();

            return author == null ? "null" : 
                string.IsNullOrEmpty(author.Pseudonym) ?
                    StringUtils.GetPersonName(author.Name, author.Surname, author.Patronymic) : author.Pseudonym;
        }

        private static string GetPublisherName(int publisherId, AppDataContext db) =>
            db?.Database?.SqlQuery<Publisher>($"select * from {nameof(db.Publishers)} where Id={publisherId}")
              ?.ToList()
              ?.First()
              ?.Name ?? "null";

        private static string GetGenreName(int genreId, AppDataContext db) =>
            db?.Database?.SqlQuery<Genre>($"select * from {nameof(db.Genres)} where Id={genreId}")
              ?.ToList()
              ?.First()
              ?.Name ?? "null";

        public override string ToString() => $"\"{this.Name}\"";

        public int    Id        { get; set; }

        [DisplayName("Название")]
        public string Name      { get; set; }

        [DisplayName("Автор")]
        public string Author    { get; set; }

        [DisplayName("Издание")]
        public string Publisher { get; set; }

        [DisplayName("Жанр")]
        public string Genre     { get; set; }

        [DisplayName("Активность")]
        public bool   IsActive  { get; set; }
    }
}
