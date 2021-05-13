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
    public class AddFacultyAndSpecialtyAndCathedraViewModel : AddModelViewModel {
        public AddFacultyAndSpecialtyAndCathedraViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.FacultyAndSpecialtyAndCathedras) }')").ToList().FirstOrDefault() ?? 0) + 1;
            this.IsActive = true;
            this.InitTitles(StringConst.Adding, $"{StringConst.Adding} связи факультет-специальность-кафедра", StringConst.Add);
        }

        public AddFacultyAndSpecialtyAndCathedraViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
            this.InitTitles(StringConst.Editing, $"{StringConst.Editing} связи факультет-специальность-кафедра", StringConst.Edit);
        }

        public int Id                           { get; set; }

        public List<FacultyAndSpecialtyViewModel> FacultyAndSpecialties { get; set; } = new FacultyAndSpecialtyDealer().Select(GlobalAppDataContext.Instance).Select(c => new FacultyAndSpecialtyViewModel(c, GlobalAppDataContext.Instance)).ToList();
        public int SelectedFacultyAndSpecialtyIndex                     { get; set; }

        public List<CathedraViewModel> Cathedras                        { get; set; } = new CathedraDealer().Select(GlobalAppDataContext.Instance).Select(c => new CathedraViewModel(c)).ToList();
        public int SelectedCathedraIndex                                { get; set; }

        public bool IsActive { get; set; }

        protected override void Add() {
            try {
                new FacultyAndSpecialtyAndCathedraDealer().AddFacultyAndSpecialtyAndCathedra(GlobalAppDataContext.Instance, this.FacultyAndSpecialties[this.SelectedCathedraIndex].Id, this.Cathedras[this.SelectedCathedraIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new FacultyAndSpecialtyAndCathedraDealer().UpdateFacultyAndSpecialtyAndCathedra(GlobalAppDataContext.Instance, this.Id, this.FacultyAndSpecialties[this.SelectedCathedraIndex].Id, this.Cathedras[this.SelectedCathedraIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            try {
                var facultyAndSpecialtyAndCathedra = new FacultyAndSpecialtyAndCathedraDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
                if (facultyAndSpecialtyAndCathedra is null) {
                    return;
                }

                var tempFacultyAndSpecialty = new FacultyAndSpecialtyViewModel(new FacultyAndSpecialtyDealer().Select(GlobalAppDataContext.Instance, facultyAndSpecialtyAndCathedra.FacultyAndSpecialtyId).First(), GlobalAppDataContext.Instance);
                var i = 0;
                foreach (var a in this.FacultyAndSpecialties) {
                    if (a.Id == tempFacultyAndSpecialty.Id) {
                        this.SelectedFacultyAndSpecialtyIndex = i;
                        break;
                    }
                    ++i;
                }

                var tempCathedra = new CathedraViewModel(new CathedraDealer().Select(GlobalAppDataContext.Instance, facultyAndSpecialtyAndCathedra.CathedraId).First());
                i = 0;
                foreach (var a in this.Cathedras) {
                    if (a.Id == tempCathedra.Id) {
                        this.SelectedCathedraIndex = i;
                        break;
                    }
                    ++i;
                }

                this.IsActive = facultyAndSpecialtyAndCathedra.IsActive;
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Get all data failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
