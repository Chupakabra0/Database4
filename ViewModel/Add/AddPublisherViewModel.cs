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
    public class AddPublisherViewModel : AddModelViewModel {
        public AddPublisherViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Publishers) }')").ToList().FirstOrDefault() ?? 0) + 1;
            this.IsActive = true;
            this.InitTitles(StringConst.Adding, $"{StringConst.Adding} издания", StringConst.Add);

        }

        public AddPublisherViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
            this.InitTitles(StringConst.Editing, $"{StringConst.Editing} издания", StringConst.Edit);
        }

        public int     Id                 { get; set; }
        public string  Name               { get; set; }
        public List<CityViewModel> Cities { get; set; } = new CityDealer().Select(GlobalAppDataContext.Instance).Select(c => new CityViewModel(c)).ToList();
        public int     SelectedCityIndex  { get; set; }
        public bool    IsActive           { get; set; }

        protected override void Add() {
            try {
                new PublisherDealer().AddPublisher(GlobalAppDataContext.Instance, this.Name, this.Cities[this.SelectedCityIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new PublisherDealer().UpdatePublisher(GlobalAppDataContext.Instance, this.Id, this.Name, this.Cities[this.SelectedCityIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            try {
                var publisher = new PublisherDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
                if (publisher is null) {
                    return;
                }
                var temp = new CityViewModel(new CityDealer().Select(GlobalAppDataContext.Instance, publisher.CityId).First());

                this.Name = publisher.Name;
                var i = 0;
                foreach (var a in this.Cities) {
                    if (a.Id == temp.Id) {
                        this.SelectedCityIndex = i;
                        break;
                    }
                    ++i;
                }

                this.IsActive = publisher.IsActive;
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Get all data failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
