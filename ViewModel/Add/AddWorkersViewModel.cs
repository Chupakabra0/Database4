using System;
using System.Windows;
using ConsoleDBTest.Dealer;
using Database4.Data;
using PropertyChanged;
using System.Linq;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public class AddWorkersViewModel : AddModelViewModel {
        public AddWorkersViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Workers) }')").ToList().FirstOrDefault() ?? 0) + 1;
            this.IsActive = true;
            this.InitTitles(StringConst.Adding, $"{StringConst.Adding} сотрудника", StringConst.Add);
        }

        public AddWorkersViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
            this.InitTitles(StringConst.Editing, $"{StringConst.Editing} сотрудника", StringConst.Edit);
        }

        public int     Id          { get; set; }
        public string  Name        { get; set; }
        public string  Surname     { get; set; }
        public string  Patronymic  { get; set; }
        public bool    IsActive    { get; set; }

        protected override void Add() {
            try {
                new WorkerDealer().AddWorker(GlobalAppDataContext.Instance, this.Name, this.Surname, this.Patronymic, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new WorkerDealer().UpdateWorker(GlobalAppDataContext.Instance, this.Id, this.Name, this.Surname, this.Patronymic, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            var author = new WorkerDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
            if (author is null) {
                return;
            }

            this.Name       = author.Name;
            this.Surname    = author.Surname;
            this.Patronymic = author.Patronymic;
            this.IsActive   = author.IsActive;
        }
    }
}
