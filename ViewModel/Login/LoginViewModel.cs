using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ConsoleDBTest.Models;
using Database4.Data;
using PropertyChanged;

namespace Database4.ViewModel.Login {
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel : ViewModel {
        public string Name       { get; set; } = string.Empty;
        public string Surname    { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;

        public ICommand LoginCommand => new RelayParameterCommand(o => this.Login(o as Window));

        private static string AdminCode { get; } = "admin"; 

        // TODO: chains of respons
        private void Login(Window loginWindow) {
            try {
                if (this.CheckAdminData()) {
                    var window = new AdminWindow();
                    loginWindow.Hide();
                    window.DataContext = new AdminViewModel(true);
                    window.ShowDialog();
                    loginWindow.Close();
                }
                else if (this.CheckWorkerData()) {
                    var window = new AdminWindow();
                    loginWindow.Hide();
                    window.DataContext = new AdminViewModel(false);
                    window.ShowDialog();
                    loginWindow.Close();
                }
                else {
                    MessageBox.Show("Неправильное имя/фамилия/отчество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception exception) {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckAdminData() =>
            this.Name == LoginViewModel.AdminCode
            &&
            this.Surname == LoginViewModel.AdminCode
            &&
            this.Patronymic == LoginViewModel.AdminCode;

        private bool CheckWorkerData() {
            var workersIds = GlobalAppDataContext.Instance.Database
                .SqlQuery<int>($"select Id from { nameof(AppDataContext.Workers) } where Name=N'{ this.Name }' and Surname=N'{ this.Surname }' and Patronymic=N'{ this.Patronymic }' and IsActive='true'")
                .ToListAsync();

            return workersIds.Result.Count != 0;
        }
    }
}
