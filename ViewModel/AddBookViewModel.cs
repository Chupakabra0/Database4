using System;
using System.Windows;
using ConsoleDBTest.Dealer;
using Database4.Data;
using PropertyChanged;
using System.Linq;
using System.Collections.Generic;
using ConsoleDBTest.ViewModels;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public class AddBookViewModel : AddModelViewModel {
        public AddBookViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Books) }')").ToList().FirstOrDefault() ?? 0) + 1;
        }

        public AddBookViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
        }

        public int     Id                          { get; set; }
        public string  Name                        { get; set; }

        public List<AuthorViewModel> Authors       { get; set; } = new AuthorDealer().Select(GlobalAppDataContext.Instance).Select(c => new AuthorViewModel(c)).ToList();
        public int     SelectedAuthorIndex         { get; set; }

        public List<PublisherViewModel> Publishers { get; set; } = new PublisherDealer().Select(GlobalAppDataContext.Instance).Select(c => new PublisherViewModel(c)).ToList();
        public int SelectedPublisherIndex          { get; set; }

        public List<GenreViewModel> Genres         { get; set; } = new GenreDealer().Select(GlobalAppDataContext.Instance).Select(c => new GenreViewModel(c)).ToList();
        public int SelectedGenreIndex              { get; set; }

        public bool IsActive { get; set; }

        protected override void Add() {
            try {
                new BookDealer().AddBook(GlobalAppDataContext.Instance, this.Name, this.Authors[this.SelectedAuthorIndex].Id, this.Publishers[this.SelectedPublisherIndex].Id, this.Genres[this.SelectedGenreIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new BookDealer().UpdateBook(GlobalAppDataContext.Instance, this.Id, this.Name, this.Authors[this.SelectedAuthorIndex].Id, this.Publishers[this.SelectedPublisherIndex].Id, this.Genres[this.SelectedGenreIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            try {
                var book = new BookDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
                if (book is null) {
                    return;
                }
                this.Name = book.Name;

                var tempAuthor = new AuthorViewModel(new AuthorDealer().Select(GlobalAppDataContext.Instance, book.AuthorId).First());
                var i = 0;
                foreach (var a in this.Authors) {
                    if (a.Id == tempAuthor.Id) {
                        this.SelectedAuthorIndex = i;
                        break;
                    }
                    ++i;
                }

                var tempGenre = new GenreViewModel(new GenreDealer().Select(GlobalAppDataContext.Instance, book.GenreId).First());
                i = 0;
                foreach (var a in this.Genres) {
                    if (a.Id == tempGenre.Id) {
                        this.SelectedGenreIndex = i;
                        break;
                    }
                    ++i;
                }

                var tempPublisher = new PublisherViewModel(new PublisherDealer().Select(GlobalAppDataContext.Instance, book.PublisherId).First());
                i = 0;
                foreach (var a in this.Publishers) {
                    if (a.Id == tempPublisher.Id) {
                        this.SelectedPublisherIndex = i;
                        break;
                    }
                    ++i;
                }

                this.IsActive = book.IsActive;
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Get all data failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
