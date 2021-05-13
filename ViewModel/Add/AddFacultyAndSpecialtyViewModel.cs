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
    public class AddFacultyAndSpecialtyViewModel : AddModelViewModel {
        public AddFacultyAndSpecialtyViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.FacultyAndSpecialties) }')").ToList().FirstOrDefault() ?? 0) + 1;
        }

        public AddFacultyAndSpecialtyViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
        }

        public int     Id                           { get; set; }

        public List<FacultyViewModel> Faculties     { get; set; } = new FacultyDealer().Select(GlobalAppDataContext.Instance).Select(c => new FacultyViewModel(c)).ToList();
        public int SelectedFacultyIndex             { get; set; }

        public List<SpecialtyViewModel> Specialties { get; set; } = new SpecialtyDealer().Select(GlobalAppDataContext.Instance).Select(c => new SpecialtyViewModel(c)).ToList();
        public int SelectedSpecialtyIndex           { get; set; }

        public bool IsActive { get; set; }

        protected override void Add() {
            try {
                new FacultyAndSpecialtyDealer().AddFacultyAndSpecialty(GlobalAppDataContext.Instance, this.Faculties[this.SelectedFacultyIndex].Id, this.Specialties[this.SelectedSpecialtyIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new FacultyAndSpecialtyDealer().UpdateFacultyAndSpecialty(GlobalAppDataContext.Instance, this.Id, this.Faculties[this.SelectedFacultyIndex].Id, this.Specialties[this.SelectedSpecialtyIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            try {
                var facultyAndSpecialty = new FacultyAndSpecialtyDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
                if (facultyAndSpecialty is null) {
                    return;
                }

                var tempFaculty = new FacultyViewModel(new FacultyDealer().Select(GlobalAppDataContext.Instance, facultyAndSpecialty.FacultyId).First());
                var i = 0;
                foreach (var a in this.Faculties) {
                    if (a.Id == tempFaculty.Id) {
                        this.SelectedFacultyIndex = i;
                        break;
                    }
                    ++i;
                }

                var tempSpecialty = new SpecialtyViewModel(new SpecialtyDealer().Select(GlobalAppDataContext.Instance, facultyAndSpecialty.SpecialtyId).First());
                i = 0;
                foreach (var a in this.Specialties) {
                    if (a.Id == tempSpecialty.Id) {
                        this.SelectedSpecialtyIndex = i;
                        break;
                    }
                    ++i;
                }

                this.IsActive = facultyAndSpecialty.IsActive;
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Get all data failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
