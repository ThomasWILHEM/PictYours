using PictYours.utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PictYours.converters
{
    public class String2Image : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string typeChemin = parameter as string;
            string imageName = value as string;
            if (imageName == null || typeChemin == null) return null;

            string imagePath = null;
            switch (typeChemin)
            {
                case "Profil":
                    imagePath = Path.Combine(GestionImage.ProfilsPath,imageName);
                    break;
                case "Images":
                    imagePath = Path.Combine(GestionImage.ImagesPath,imageName);
                    break;
            }
            if (string.IsNullOrWhiteSpace(imagePath)) return null;

            return new Uri(imagePath, UriKind.RelativeOrAbsolute);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
