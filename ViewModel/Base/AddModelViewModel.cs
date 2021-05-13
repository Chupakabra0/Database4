using System.Windows;
using System.Windows.Input;
using PropertyChanged;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public abstract class AddModelViewModel : ViewModel {
        public AddModelViewModel(Window window) {
            this.windowReference_ = window;
        }

        public string   TitleWindow     { get; set; }
        public string   HeaderWindow    { get; set; }
        public string   AddButtonText   { get; set; }
        public ICommand AddCommand      { get; protected set; }

        protected Window windowReference_;

        protected void InitTitles(string titleWindow, string header, string buttonText) {
            this.TitleWindow   = titleWindow;
            this.HeaderWindow  = header;
            this.AddButtonText = buttonText;
        }

        abstract protected void Add();
        abstract protected void Edit();
        abstract protected void GetAllData(int id);
    }
}