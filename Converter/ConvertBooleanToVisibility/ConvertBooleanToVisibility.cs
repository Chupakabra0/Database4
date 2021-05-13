using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Database4.Converter.ConvertBooleanToVisibility {
    public class ConvertBooleanToVisibility : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            System.Convert.ToBoolean(value ?? false) ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
             value switch {
                 Visibility.Visible => true,
                 _                  => false
             };
    }
}
