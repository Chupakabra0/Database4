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
    public class AddGroupViewModel : AddModelViewModel {
        public AddGroupViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.Groups) }')").ToList().FirstOrDefault() ?? 0) + 1;
            this.Year = DateTime.Today.Year;
            this.Serial = 1;
        }

        public AddGroupViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
        }

        public int Id     { get; set; }
        public int Year   { get; set; }
        public int Serial { get; set; }

        public List<FacultyAndSpecialtyAndCathedraViewModel> FacultyAndSpecialtyAndCathedras { get; set; } = new FacultyAndSpecialtyAndCathedraDealer().Select(GlobalAppDataContext.Instance).Select(c => new FacultyAndSpecialtyAndCathedraViewModel(c, GlobalAppDataContext.Instance)).ToList();
        public int SelectedFacultyAndSpecialtyAndCathedraIndex { get; set; }

        public List<DegreeViewModel> Degrees { get; set; } = new DegreeDealer().Select(GlobalAppDataContext.Instance).Select(c => new DegreeViewModel(c)).ToList();
        public int SelectedDegreeIndex       { get; set; }

        public bool IsActive { get; set; }

        protected override void Add() {
            try {
                new GroupDealer().AddGroup(GlobalAppDataContext.Instance, this.FacultyAndSpecialtyAndCathedras[this.SelectedFacultyAndSpecialtyAndCathedraIndex].Id, this.Degrees[this.SelectedDegreeIndex].Id, this.Year, this.Serial, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new GroupDealer().UpdateGroup(GlobalAppDataContext.Instance, this.Id, this.FacultyAndSpecialtyAndCathedras[this.SelectedFacultyAndSpecialtyAndCathedraIndex].Id, this.Degrees[this.SelectedDegreeIndex].Id, this.Year, this.Serial, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            try {
                var group = new GroupDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
                if (group is null) {
                    return;
                }

                this.Year   = group.Year;
                this.Serial = group.Serial;

                var tempFacultyAndSpecialtyAndCathedra = new FacultyAndSpecialtyAndCathedraViewModel(new FacultyAndSpecialtyAndCathedraDealer().Select(GlobalAppDataContext.Instance, group.FacultyAndSpecialtyAndCathedraId).First(), GlobalAppDataContext.Instance);
                var i = 0;
                foreach (var a in this.FacultyAndSpecialtyAndCathedras) {
                    if (a.Id == tempFacultyAndSpecialtyAndCathedra.Id) {
                        this.SelectedFacultyAndSpecialtyAndCathedraIndex = i;
                        break;
                    }
                    ++i;
                }

                var tempDegree = new DegreeViewModel(new DegreeDealer().Select(GlobalAppDataContext.Instance, group.DegreeId).First());
                i = 0;
                foreach (var a in this.Degrees) {
                    if (a.Id == tempDegree.Id) {
                        this.SelectedDegreeIndex = i;
                        break;
                    }
                    ++i;
                }

                this.IsActive = group.IsActive;
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Get all data failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
