using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Database4.Data;

namespace Database4.Converter.GetButtonStateByHeader {
    public class GetButtonStateByHeader : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value as string switch {
                nameof(AppDataContext.ClientCards)          => Visibility.Visible,
                nameof(AppDataContext.LibraryTransactions)  => Visibility.Visible,
                _                                           => Visibility.Collapsed
            };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => new NotSupportedException("Convertation from Visibility to Header isn't available");
    }
}
