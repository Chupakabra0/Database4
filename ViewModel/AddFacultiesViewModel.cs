using System;
using System.Windows;
using ConsoleDBTest.Dealer;
using Database4.Data;
using PropertyChanged;
using System.Linq;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public class AddFacultiesViewModel : AddModelViewModel {
        public AddFacultiesViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                 ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Faculties) }')").ToList().FirstOrDefault() ?? 0) + 1;
        }

        public AddFacultiesViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
        }

        public int    Id          { get; set; }
        public string Name        { get; set; }
        public string ShortName   { get; set; }
        public string ShortLetter { get; set; }
        public bool   IsActive    { get; set; }

        protected override void Add() {
            try {
                new FacultyDealer().AddFaculty(GlobalAppDataContext.Instance, this.Name, this.ShortName, this.ShortLetter, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new FacultyDealer().UpdateFaculty(GlobalAppDataContext.Instance, this.Id, this.Name, this.ShortName, this.ShortLetter, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            var faculty = new FacultyDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
            if (faculty is null) {
                return;
            }

            this.Name        = faculty.Name;
            this.ShortName   = faculty.ShortName;
            this.ShortLetter = faculty.ShortLetter;
            this.IsActive    = faculty.IsActive;
        }
    }
}
