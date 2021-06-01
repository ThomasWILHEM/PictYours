using System;
using System.Globalization;
using System.Windows.Data;

namespace PictYours.converters
{
    /// <summary>
    /// Convertit un nombre de visite pour qu'il apparaisse dans le format "Nombre de visites : {nb}"
    /// </summary>
    public class NombreVisites2Affichage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"Nombre de visites: {(int)value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
