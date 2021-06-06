using BiblioClasse;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PictYours.converters
{
    /// <summary>
    /// Permet de gérer la visibilité du bouton pour modifier le profil en fonction de l'utilisateur actuel
    /// </summary>
    public class Utilisateur2ModificationProfilButton : IValueConverter
    {
        public Manager LeManager => (App.Current as App).LeManager;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "Collapsed";
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
