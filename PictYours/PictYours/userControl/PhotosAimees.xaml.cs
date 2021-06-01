using BiblioClasse;
using System.Windows.Controls;

namespace PictYours.userControl
{
    /// <summary>
    /// Logique d'interaction pour PhotosLikées.xaml
    /// </summary>
    public partial class PhotosAimees : UserControl
    {
        /// <summary>
        /// Manager de l'application
        /// </summary>
        Manager LeManager = (App.Current as App).LeManager;

        /// <summary>
        /// Constructeur de la page PhotoAimees
        /// </summary>
        public PhotosAimees()
        {
            InitializeComponent();
            DataContext = LeManager.ManagerUtilisateur.UtilisateurActuel as Amateur;
        }

        /// <summary>
        /// Evenement lié au changement de sélection d'élement dans la ListBox "ListeBoxPhotosAimees"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListeBoxPhotosAimees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                LeManager.ManagerPhoto.PhotoSelectionne = null;
                return;
            }
            LeManager.ManagerPhoto.PhotoSelectionne = e.AddedItems[0] as Photo;
        }
    }
}
