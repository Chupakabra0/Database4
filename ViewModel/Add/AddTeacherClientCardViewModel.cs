﻿using System;
using System.Windows;
using ConsoleDBTest.Dealer;
using Database4.Data;
using PropertyChanged;
using System.Linq;
using System.Collections.Generic;
using ConsoleDBTest.ViewModels;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public class AddTeacherClientCardViewModel : AddModelViewModel {
        public AddTeacherClientCardViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.ClientCards) }')").ToList().FirstOrDefault() ?? 0) + 1;
            this.Date = DateTime.Today;
            this.IsActive = true;
            this.InitTitles(StringConst.Adding, $"{StringConst.Adding} преподавателя", StringConst.Add);
        }

        public AddTeacherClientCardViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
            this.InitTitles(StringConst.Editing, $"{StringConst.Editing} преподавателя", StringConst.Edit);
        }

        public int Id                              { get; set; }
        public DateTime? Date                      { get; set; }

        public List<TeacherViewModel> Persons      { get; set; } = new TeacherDealer().Select(GlobalAppDataContext.Instance).Select(c => new TeacherViewModel(c, GlobalAppDataContext.Instance)).ToList();
        public int SelectedPersonIndex             { get; set; }

        public bool IsActive { get; set; }

        protected override void Add() {
            try {
                new ClientCardDealer().AddCard(GlobalAppDataContext.Instance, this.Date, null, this.Persons[this.SelectedPersonIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new ClientCardDealer().UpdateCard(GlobalAppDataContext.Instance, this.Id, this.Date, null, this.Persons[this.SelectedPersonIndex].Id, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            try {
                var card = new ClientCardDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
                if (card is null) {
                    return;
                }

                this.Date = card.DateGiven;

                var tempTeacher = new TeacherViewModel(new TeacherDealer().Select(GlobalAppDataContext.Instance, card.TeacherId ?? 0).First(), GlobalAppDataContext.Instance);
                var i = 0;
                foreach (var a in this.Persons) {
                    if (a.Id == tempTeacher.Id) {
                        this.SelectedPersonIndex = i;
                        break;
                    }
                    ++i;
                }

                this.IsActive = card.IsActive;
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Get all data failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
