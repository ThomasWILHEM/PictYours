using BiblioClasse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PictYours.userControl
{
    /// <summary>
    /// Logique d'interaction pour PhotosLikées.xaml
    /// </summary>
    public partial class PhotosAimees : UserControl
    {

        Manager LeManager = (App.Current as App).LeManager;

        public PhotosAimees()
        {
            InitializeComponent();
            DataContext = LeManager.ManagerUtilisateur.UtilisateurActuel as Amateur;
        }

        private void ListeBoxPhotosAimees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                LeManager.ManagerPhoto.PhotoSelectionne = null;
                return;
            }
            LeManager.ManagerPhoto.PhotoSelectionne = e.AddedItems[0] as BiblioClasse.Photo;
        }
    }
}
