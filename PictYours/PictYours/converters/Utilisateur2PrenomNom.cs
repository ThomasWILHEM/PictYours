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
    public class Utilisateur2PrenomNom : IValueConverter
    {
        public Manager LeManager => (App.Current as App).LeManager;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (value.Equals(LeManager.ManagerUtilisateur.UtilisateurActuel))
            {
                if (value is Amateur amateur)
                {
                    return $"{amateur.Prenom} {amateur.Nom} (Moi)";
                }
                if (value is Commercial commercial)
                {
                    return $"{commercial.Nom} (Moi)";
                }
            }
            else
            {
                if (value is Amateur amateur)
                {
                    return $"{amateur.Prenom} {amateur.Nom}";
                }
                if (value is Commercial commercial)
                {
                    return $"{commercial.Nom}";
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
