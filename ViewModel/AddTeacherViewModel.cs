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
    public class AddTeacherViewModel : AddModelViewModel {
        public AddTeacherViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Teachers) }')").ToList().FirstOrDefault() ?? 0) + 1;
        }

        public AddTeacherViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
        }

        public int     Id         { get; set; }
        public string  Name       { get; set; }
        public string  Surname    { get; set; }
        public string  Patronymic { get; set; }

        public List<CityViewModel> Cities  { get; set; } = new CityDealer().Select(GlobalAppDataContext.Instance).Select(c => new CityViewModel(c, GlobalAppDataContext.Instance)).ToList();
        public int      SelectedCityIndex  { get; set; }

        public List<CathedraViewModel> Cathedras { get; set; } = new CathedraDealer().Select(GlobalAppDataContext.Instance).Select(c => new CathedraViewModel(c)).ToList();
        public int SelectedCathedraIndex         { get; set; }

        public bool IsActive { get; set; }

        protected override void Add() {
            try {
                new TeacherDealer().AddTeacher(GlobalAppDataContext.Instance, this.Name, this.Surname, this.Patronymic, this.Cities[this.SelectedCityIndex].Id, this.Cathedras[this.SelectedCathedraIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new TeacherDealer().UpdateTeacher(GlobalAppDataContext.Instance, this.Id, this.Name, this.Surname, this.Patronymic, this.Cities[this.SelectedCityIndex].Id, this.Cathedras[this.SelectedCathedraIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            try {
                var teacher = new TeacherDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
                if (teacher is null) {
                    return;
                }

                this.Name       = teacher.Name;
                this.Surname    = teacher.Surname;
                this.Patronymic = teacher.Patronymic;

                var tempCity = new CityViewModel(new CityDealer().Select(GlobalAppDataContext.Instance, teacher.CityId).First(), GlobalAppDataContext.Instance);
                var i = 0;
                foreach (var a in this.Cities) {
                    if (a.Id == tempCity.Id) {
                        this.SelectedCityIndex = i;
                        break;
                    }
                    ++i;
                }

                var tempCathedra = new CathedraViewModel(new CathedraDealer().Select(GlobalAppDataContext.Instance, teacher.CathedraId).First());
                i = 0;
                foreach (var a in this.Cathedras) {
                    if (a.Id == tempCathedra.Id) {
                        this.SelectedCathedraIndex = i;
                        break;
                    }
                    ++i;
                }

                this.IsActive = teacher.IsActive;
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Get all data failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
