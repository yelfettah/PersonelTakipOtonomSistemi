using System.Globalization;

namespace PersonelTakipOtonomSistemi.Converters
{
    public class DurumRenkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string durum)
            {
                return durum == "Aktif" ? Color.FromHex("#FFFFFF") : Color.FromHex("#FFF5F5");
            }
            return Color.FromHex("#FFFFFF");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DurumBaslikRenkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string durum)
            {
                return durum == "Aktif" ? Color.FromHex("#E8F4FD") : Color.FromHex("#FFE8E8");
            }
            return Color.FromHex("#E8F4FD");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DurumEtiketRenkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string durum)
            {
                return durum == "Aktif" ? Color.FromHex("#28A745") : Color.FromHex("#DC3545");
            }
            return Color.FromHex("#6C757D");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 