using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Database4.Data;

namespace Database4.Converter.GetControlHeader {
    public class GetControlHeader : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => new NotSupportedException("Convertation from Name to Control isn't available");

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => (value as HeaderedContentControl)?.Header;
    }
}
