using System;
using System.Windows;
using ConsoleDBTest.Dealer;
using Database4.Data;
using PropertyChanged;
using System.Linq;
using System.Collections.Generic;
using ConsoleDBTest.ViewModels;
using ConsoleDBTest.Models;
using ConsoleDBTest.Utils.StringUtils;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public class AddLibraryTransactionViewModel : AddModelViewModel {
        public AddLibraryTransactionViewModel(Window windowRef) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Add);
            this.Id = Convert.ToInt32(GlobalAppDataContext.Instance.Database.SqlQuery<int?>
                ($"select last_value from sys.identity_columns as a where object_id = object_id('{ nameof(AppDataContext.LibraryTransactions) }')").ToList().FirstOrDefault() ?? 0) + 1;
            this.TakeDate = DateTime.Today;
            this.ReturnDate = DateTime.Today;
            this.IsActive = true;
            this.InitTitles(StringConst.Adding, $"{StringConst.Adding} сделки", StringConst.Add);
        }

        public AddLibraryTransactionViewModel(Window windowRef, int id) : base(windowRef) {
            this.AddCommand = new RelayCommand(this.Edit);
            this.Id = id;
            this.GetAllData(this.Id);
            this.InitTitles(StringConst.Editing, $"{StringConst.Editing} сделки", StringConst.Edit);
        }

        public int Id                              { get; set; }
        public DateTime? TakeDate                  { get; set; }
        public DateTime? ReturnDate                { get; set; }

        public List<ClientCardViewModel> Clients   { get; set; } = new ClientCardDealer().Select(GlobalAppDataContext.Instance).Select(c => new ClientCardViewModel(c, GlobalAppDataContext.Instance)).ToList();
        public int SelectedClientIndex             { get; set; }

        public List<BookViewModel> Books           { get; set; } = new BookDealer().Select(GlobalAppDataContext.Instance).Select(c => new BookViewModel(c, GlobalAppDataContext.Instance)).ToList();
        public int SelectedBookIndex               { get; set; }

        public List<WorkerViewModel> Workers       { get; set; } = new WorkerDealer().Select(GlobalAppDataContext.Instance).Select(c => new WorkerViewModel(c)).ToList();
        public int SelectedWorkerIndex             { get; set; }

        public bool IsActive                       { get; set; }
        // TIP: if client isn't Teacher
        public bool IsInTime                       => this.Clients[this.SelectedClientIndex].ClientTypeStr == ClientType.Student.ClientTypeToString();

        protected override void Add() {
            try {
                new LibraryTransactionDealer().AddTransaction(GlobalAppDataContext.Instance, this.TakeDate, this.ReturnDate, this.Clients[this.SelectedClientIndex].Id, this.Workers[this.SelectedWorkerIndex].Id, this.Books[this.SelectedBookIndex].Id, this.IsInTime, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Добавлено!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Adding failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void Edit() {
            try {
                new LibraryTransactionDealer().UpdateTransaction(GlobalAppDataContext.Instance, this.Id, this.TakeDate, this.ReturnDate, this.Clients[this.SelectedClientIndex].Id, this.Workers[this.SelectedWorkerIndex].Id, this.Books[this.SelectedBookIndex].Id, false, this.IsActive);
                this.windowReference_.DialogResult = MessageBox.Show("Готово!", "Отредактировано!", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
                this.windowReference_.Close();
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Editing failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void GetAllData(int id) {
            try {
                var transaction = new LibraryTransactionDealer().Select(GlobalAppDataContext.Instance, id).FirstOrDefault();
                if (transaction is null) {
                    return;
                }

                this.TakeDate   = transaction.TakeDate;
                this.ReturnDate = transaction.ReturnDate;

                var tempClient = new ClientCardViewModel(new ClientCardDealer().Select(GlobalAppDataContext.Instance, transaction.ClientCardId).First(), GlobalAppDataContext.Instance);
                var i = 0;
                foreach (var a in this.Clients) {
                    if (a.Id == tempClient.Id) {
                        this.SelectedClientIndex = i;
                        break;
                    }
                    ++i;
                }

                var tempBook = new BookViewModel(new BookDealer().Select(GlobalAppDataContext.Instance, transaction.BookId).First(), GlobalAppDataContext.Instance);
                i = 0;
                foreach (var a in this.Books) {
                    if (a.Id == tempBook.Id) {
                        this.SelectedBookIndex = i;
                        break;
                    }
                    ++i;
                }

                var tempWorker = new WorkerViewModel(new WorkerDealer().Select(GlobalAppDataContext.Instance, transaction.WorkerId).First());
                i = 0;
                foreach (var a in this.Workers) {
                    if (a.Id == tempWorker.Id) {
                        this.SelectedWorkerIndex = i;
                        break;
                    }
                    ++i;
                }

                this.IsActive = transaction.IsActive;
            }
            catch (Exception) {
                MessageBox.Show("Error!", "Get all data failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
