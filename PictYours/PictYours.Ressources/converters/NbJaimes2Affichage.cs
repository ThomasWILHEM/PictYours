using System;
using System.Globalization;
using System.Windows.Data;

namespace PictYours.Ressources.converters
{
    /// <summary>
    /// Convertit un nombre de J'aime pour qu'il apparaisse dans le format "{nb} j'aime(s)"
    /// </summary>
    public class NbJaimes2Affichage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            int i = (int)value;
            if (i <= 1) return $"{i} j'aime";
            return $"{i} j'aimes";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
