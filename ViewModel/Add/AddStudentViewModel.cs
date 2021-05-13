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
    public class AddStudentViewModel : AddModelViewModel {
        public AddStudentViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Students) }')").ToList().FirstOrDefault() ?? 0) + 1;
            this.IsActive = true;
        }

        public AddStudentViewModel(Window windowRef, int id) : base(windowRef) {
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

        public List<GroupViewModel> Groups { get; set; } = new GroupDealer().Select(GlobalAppDataContext.Instance).Select(c => new GroupViewModel(c, GlobalAppDataContext.Instance)).ToList();
        public int SelectedGroupIndex      { get; set; }

        public bool IsActive { get; set; }

        protected override void Add() {
            try {
                new StudentDealer().AddStudent(GlobalAppDataContext.Instance, this.Name, this.Surname, this.Patronymic, this.Cities[this.SelectedCityIndex].Id, this.Groups[this.SelectedGroupIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new StudentDealer().UpdateStudent(GlobalAppDataContext.Instance, this.Id, this.Name, this.Surname, this.Patronymic, this.Cities[this.SelectedCityIndex].Id, this.Groups[this.SelectedGroupIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            try {
                var student = new StudentDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
                if (student is null) {
                    return;
                }

                this.Name       = student.Name;
                this.Surname    = student.Surname;
                this.Patronymic = student.Patronymic;

                var tempCity = new CityViewModel(new CityDealer().Select(GlobalAppDataContext.Instance, student.CityId).First(), GlobalAppDataContext.Instance);
                var i = 0;
                foreach (var a in this.Cities) {
                    if (a.Id == tempCity.Id) {
                        this.SelectedCityIndex = i;
                        break;
                    }
                    ++i;
                }

                var tempGroup = new GroupViewModel(new GroupDealer().Select(GlobalAppDataContext.Instance, student.GroupId).First(), GlobalAppDataContext.Instance);
                i = 0;
                foreach (var a in this.Groups) {
                    if (a.Id == tempGroup.Id) {
                        this.SelectedGroupIndex = i;
                        break;
                    }
                    ++i;
                }

                this.IsActive = student.IsActive;
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Get all data failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
