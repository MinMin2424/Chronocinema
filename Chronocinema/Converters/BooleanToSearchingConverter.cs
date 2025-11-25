using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Chronocinema.Converters
{
    public class BooleanToSearchingConverter : IValueConverter
    {
        public static BooleanToSearchingConverter Instance { get; } = new BooleanToSearchingConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && (bool)value ? "Searching..." : "Add";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
