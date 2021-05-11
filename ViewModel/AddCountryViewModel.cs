using System;
using System.Windows;
using ConsoleDBTest.Dealer;
using Database4.Data;
using PropertyChanged;
using System.Linq;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public class AddCountryViewModel : AddModelViewModel {
        public AddCountryViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Countries) }')").ToList().FirstOrDefault() ?? 0) + 1;
        }

        public AddCountryViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
        }

        public int     Id        { get; set; }
        public string  LongName  { get; set; }
        public string  ShortName { get; set; }
        public string  ISOCode   { get; set; }
        public bool    IsActive  { get; set; }

        protected override void Add() {
            try {
                new CountryDealer().AddCountry(GlobalAppDataContext.Instance, this.LongName, this.ShortName, this.ISOCode, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new CountryDealer().UpdateCountry(GlobalAppDataContext.Instance, this.Id, this.LongName, this.ShortName, this.ISOCode, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            var country = new CountryDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
            if (country is null) {
                return;
            }

            this.LongName  = country.LongName;
            this.ShortName = country.ShortName;
            this.ISOCode   = country.ISOCode;
            this.IsActive  = country.IsActive;
        }
    }
}
