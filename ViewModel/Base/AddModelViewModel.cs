using System.Windows;
using System.Windows.Input;
using PropertyChanged;

namespace Database4.ViewModel {
    [AddINotifyPropertyChangedInterface]
    public abstract class AddModelViewModel : ViewModel {
        public AddModelViewModel(Window window) {
            this.windowReference_ = window;
        }

        public string   Title      { get; set; }
        public ICommand AddCommand { get; protected set; }

        protected Window windowReference_;

        abstract protected void Add();
        abstract protected void Edit();
        abstract protected void GetAllData(int id);
    }
}