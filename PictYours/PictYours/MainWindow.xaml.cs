using AppWpf;
using BiblioClasse;
using System;
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

        public MainWindow()
        {
            InitializeComponent();
           
            ListeUtilisateur = LeManager.ManagerUtilisateur.ListeUtilisateur;

            LeManager.ManagerPhoto.PosterUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, new Photo("/img/pp.jpg", "test", "test", LeManager.ManagerUtilisateur.UtilisateurActuel, DateTime.Today, ECategorie.Animal));
            LeManager.ManagerPhoto.PosterUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, new Photo("/img/user.png", "test156", "test123", LeManager.ManagerUtilisateur.UtilisateurActuel, DateTime.Today, ECategorie.Animal));
            LeManager.ManagerPhoto.PosterUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, new Photo("/img/estelle_rond.png", "test2", "test1532", LeManager.ManagerUtilisateur.UtilisateurActuel, DateTime.Today, ECategorie.Animal));
            LeManager.ManagerPhoto.PosterUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, new Photo("/img/vacances1.jpg", "test2", "test1532", LeManager.ManagerUtilisateur.UtilisateurActuel, DateTime.Today, ECategorie.Animal));
            LeManager.ManagerPhoto.PosterUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, new Photo("/img/vacances2.jpg", "test2", "test1532", LeManager.ManagerUtilisateur.UtilisateurActuel, DateTime.Today, ECategorie.Animal));
            LeManager.ManagerPhoto.PosterUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, new Photo("/img/vacances3.jpg", "test2", "test1532", LeManager.ManagerUtilisateur.UtilisateurActuel, DateTime.Today, ECategorie.Animal));
            LeManager.ManagerPhoto.PosterUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, new Photo("/img/vacances4.jpg", "test2", "test1532", LeManager.ManagerUtilisateur.UtilisateurActuel, DateTime.Today, ECategorie.Animal));
            LeManager.ManagerPhoto.PosterUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, new Photo("/img/vacances5.jpg", "test2", "test1532", LeManager.ManagerUtilisateur.UtilisateurActuel, DateTime.Today, ECategorie.Animal));

            DataContext = this;
        }

        //private void MenuButton_Click(object sender, RoutedEventArgs e) 
        //{
        //    if (MenuDéroulant.Visibility == Visibility.Collapsed)
        //        MenuDéroulant.Visibility = Visibility.Visible;
        //    else
        //        MenuDéroulant.Visibility = Visibility.Collapsed;
        //}

        private void ListBoxUtilisateur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LeManager.ManagerUtilisateur.UtilisateurSelectionne = e.AddedItems[0] as Utilisateur;
        }

        private void DeconnexionButton_Click(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            Close();
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ParametreButton_Click(object sender, RoutedEventArgs e)
        {
            //Affiche une nouvelle fenetre
        }

        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            LeManager.ManagerUtilisateur.UtilisateurSelectionne = LeManager.ManagerUtilisateur.UtilisateurActuel;
        }
    }
}
