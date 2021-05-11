using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using PropertyChanged;

namespace Database4.ViewModel {
    /// <summary>
    ///     Base view-model with implementation of INotifyPropertyChanged interface
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public abstract class ViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Inform view, that some property have changed
        /// </summary>
        /// <param name="propertyName">
        ///     Caller property name
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}