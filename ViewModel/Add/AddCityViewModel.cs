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
    public class AddCityViewModel : AddModelViewModel {
        public AddCityViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Cities) }')").ToList().FirstOrDefault() ?? 0) + 1;
            this.IsActive = true;
        }

        public AddCityViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
        }

        public int     Id                       { get; set; }
        public string  Name                     { get; set; }
        public List<CountryViewModel> Countries { get; set; } = new CountryDealer().Select(GlobalAppDataContext.Instance).Select(c => new CountryViewModel(c)).ToList();
        public int     SelectedCountryIndex     { get; set; }
        public bool    IsActive                 { get; set; }

        protected override void Add() {
            try {
                new CityDealer().AddCity(GlobalAppDataContext.Instance, this.Name, this.Countries[SelectedCountryIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new CityDealer().UpdateCity(GlobalAppDataContext.Instance, this.Id, this.Name, this.Countries[SelectedCountryIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            try {
                var city = new CityDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
                if (city is null) {
                    return;
                }
                var temp = new CountryViewModel(new CountryDealer().Select(GlobalAppDataContext.Instance, city.CountryId).First());

                this.Name = city.Name;
                var i = 0;
                foreach (var a in this.Countries) {
                    if (a.Id == temp.Id) {
                        this.SelectedCountryIndex = i;
                        break;
                    }
                    ++i;
                }

                this.IsActive = city.IsActive;
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Get all data failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
