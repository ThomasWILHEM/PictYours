using System;
using System.Globalization;
using System.Windows.Data;

namespace PictYours.Ressources.converters
{
    /// <summary>
    /// Convertit une date de naissance en string de type "DD/MM/YYYY"
    /// </summary>
    public class Date2Affichage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
            {
                return $"{date.ToShortDateString()}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
