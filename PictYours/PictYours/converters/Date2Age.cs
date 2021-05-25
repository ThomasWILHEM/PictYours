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
    public class Date2Age : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            DateTime date = DateTime.Parse(value.ToString());
            return $", {DateTime.Now.Year - date.Year} ans";
            //if (value is Amateur amateur)
            //{
            //    return $", {DateTime.Now.Year - amateur.DateDeNaissance.Year} ans";
            //}
            //return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
