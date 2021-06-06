using System;
using System.Globalization;
using System.Windows.Data;

namespace PictYours.Ressources.converters
{
    /// <summary>
    /// Convertit un site web pour qu'il apparaisse dans le format "Site web : {site}"
    /// </summary>
    public class Site2Affichage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                return $"Site Web : {value}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
