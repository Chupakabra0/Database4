using System;
using System.Windows;
using ConsoleDBTest.Dealer;
using Database4.Data;
using PropertyChanged;
using System.Linq;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public class AddAuthorsViewModel : AddModelViewModel {
        public AddAuthorsViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Authors) }')").ToList().FirstOrDefault() ?? 0) + 1;
            this.IsActive = true;
        }

        public AddAuthorsViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
        }

        public int     Id          { get; set; }
        public string  Name        { get; set; }
        public string  Surname     { get; set; }
        public string  Patronymic  { get; set; }
        public string  Pseudonym   { get; set; }
        public bool    IsActive    { get; set; }

        protected override void Add() {
            try {
                new AuthorDealer().AddAuthor(GlobalAppDataContext.Instance, this.Name, this.Surname, this.Patronymic, this.Pseudonym, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new AuthorDealer().UpdateAuthor(GlobalAppDataContext.Instance, this.Id, this.Name, this.Surname, this.Patronymic, this.Pseudonym, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            var author = new AuthorDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
            if (author is null) {
                return;
            }

            this.Name       = author.Name;
            this.Surname    = author.Surname;
            this.Patronymic = author.Patronymic;
            this.Pseudonym  = author.Pseudonym;
            this.IsActive   = author.IsActive;
        }
    }
}
