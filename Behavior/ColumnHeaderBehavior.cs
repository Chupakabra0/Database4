using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace Database4.Behavior {
    public class ColumnHeaderBehavior : Behavior<DataGrid> {
        protected override void OnAttached() =>
            AssociatedObject.AutoGeneratingColumn += ColumnHeaderBehavior.OnGeneratingColumn;

        protected override void OnDetaching() =>
            AssociatedObject.AutoGeneratingColumn -= ColumnHeaderBehavior.OnGeneratingColumn;

        private static void OnGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs eventArgs) {
            if (eventArgs.PropertyDescriptor is PropertyDescriptor descriptor) {
                eventArgs.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }
            else {
                eventArgs.Cancel = true;
            }
        }
    }
}
