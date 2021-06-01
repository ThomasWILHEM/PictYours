using BiblioClasse;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PictYours.converters
{
    /// <summary>
    /// Récupère une date de naissance, calcule l'age de la personne et affiche l'age dans le format "{Age} ans"
    /// </summary>
    public class Date2Age : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            DateTime date = DateTime.Parse(value.ToString());
            return $", {DateTime.Now.Year - date.Year} ans";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
