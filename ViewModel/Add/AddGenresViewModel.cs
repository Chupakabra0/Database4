using System;
using System.Windows;
using ConsoleDBTest.Dealer;
using Database4.Data;
using PropertyChanged;
using System.Linq;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public class AddGenresViewModel : AddModelViewModel {
        public AddGenresViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Genres) }')").ToList().FirstOrDefault() ?? 0) + 1;
            this.IsActive = true;
        }

        public AddGenresViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
        }

        public int    Id        { get; set; }
        public string Name      { get; set; }
        public bool   IsActive  { get; set; }

        protected override void Add() {
            try {
                new GenreDealer().AddGenre(GlobalAppDataContext.Instance, this.Name, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new GenreDealer().UpdateGenre(GlobalAppDataContext.Instance, this.Id, this.Name, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            var genre = new GenreDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
            if (genre is null) {
                return;
            }

            this.Name      = genre.Name;
            this.IsActive  = genre.IsActive;
        }
    }
}
