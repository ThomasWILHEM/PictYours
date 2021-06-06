using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace PictYours.Ressources.converters
{
    class NomPrenomAge2Affichage : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;
            StringBuilder chaine = new();
            if (!string.IsNullOrWhiteSpace(values[0] as string)) chaine.Append(values[0] as string);
            if (!string.IsNullOrWhiteSpace(values[1] as string)) chaine.Append($" {values[1] as string}");
            if (!string.IsNullOrWhiteSpace(values[2] as string)) chaine.Append(values[2] as string);
            return chaine.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
