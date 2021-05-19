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
    public class Utilisateur2ModificationProfilButton : IValueConverter
    {
        public Manager LeManager => (App.Current as App).LeManager;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals(LeManager.ManagerUtilisateur.UtilisateurActuel))
            {
                return "Visible";
            }
            return "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
