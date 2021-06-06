using System;
using System.Globalization;
using System.Windows.Data;

namespace PictYours.Ressources.converters
{
    /// <summary>
    /// Fait en sorte de rajouter un "@" devant le pseudo
    /// </summary>
    public class Pseudo2Affichage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string pseudo)
            {
                return $"@{pseudo}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
