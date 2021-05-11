using BiblioClasse;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PictYours
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Manager LeManager => (App.Current as App).LeManager;
        public ReadOnlyCollection<Utilisateur> ListeUtilisateur { get; private set; }

        public Utilisateur UtilisateurSelected; 
        public MainWindow()
        {
            InitializeComponent();
           
            ListeUtilisateur = LeManager.ManagerUtilisateur.ListeUtilisateur;

            UtilisateurSelected = LeManager.ManagerUtilisateur.UtilisateurSelectionne;
            
            DataContext = this;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuDéroulant.Visibility == Visibility.Collapsed)
                MenuDéroulant.Visibility = Visibility.Visible;
            else
                MenuDéroulant.Visibility = Visibility.Collapsed;
        }

        private void ListBoxUtilisateur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UtilisateurSelected = e.AddedItems[0] as Utilisateur;
        }
    }
}
