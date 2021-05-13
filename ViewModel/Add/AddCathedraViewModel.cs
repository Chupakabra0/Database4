using System;
using System.Windows;
using ConsoleDBTest.Dealer;
using Database4.Data;
using PropertyChanged;
using System.Linq;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public class AddCathedraViewModel : AddModelViewModel {
        public AddCathedraViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Cathedras) }')").ToList().FirstOrDefault() ?? 0) + 1;
        }

        public AddCathedraViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
        }

        public int    Id        { get; set; }
        public string Name      { get; set; }
        public string ShortName { get; set; }
        public bool   IsActive  { get; set; }

        protected override void Add() {
            try {
                new CathedraDealer().AddCathedra(GlobalAppDataContext.Instance, this.Name, this.ShortName, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new CathedraDealer().UpdateCathedra(GlobalAppDataContext.Instance, this.Id, this.Name, this.ShortName, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            var cathedra = new CathedraDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
            if (cathedra is null) {
                return;
            }

            this.Name      = cathedra.Name;
            this.ShortName = cathedra.ShortName;
            this.IsActive  = cathedra.IsActive;
        }
    }
}
