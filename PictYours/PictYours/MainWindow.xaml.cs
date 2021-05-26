﻿using AppWpf;
using BiblioClasse;
using System.Windows;

namespace PictYours
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Manager LeManager => (App.Current as App).LeManager;


        public MainWindow()
        {
            InitializeComponent();

            LeManager.ManagerPhoto.PhotoSelectionneChanged += PhotoSelectionneChanged;

            DataContext = this;
        }

        private void PhotoSelectionneChanged(object sender, ManagerPhoto.PhotoSelectionneChangedEventArgs e)
        {
            if (e.Photo != null)
            {
                PagePrincipale.Visibility = Visibility.Collapsed;
                VisualiseurPhoto.Visibility = Visibility.Visible;
                RetourButton.Visibility = Visibility.Visible;

                VisualiseurPhoto.LaPhoto = e.Photo;
            }
        }



        //private void MenuButton_Click(object sender, RoutedEventArgs e) 
        //{
        //    if (MenuDéroulant.Visibility == Visibility.Collapsed)
        //        MenuDéroulant.Visibility = Visibility.Visible;
        //    else
        //        MenuDéroulant.Visibility = Visibility.Collapsed;
        //}



        private void DeconnexionButton_Click(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            Close();
            LeManager.ManagerUtilisateur.SeDeconnecter();
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            RetourPagePrincipale();
            LeManager.ManagerUtilisateur.UtilisateurSelectionne = LeManager.ManagerUtilisateur.UtilisateurActuel;
        }

        private void CloseDialogHostButton_Click(object sender, RoutedEventArgs e)
        {
            MesParametres.ReinitialiserParametres();
        }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            RetourPagePrincipale();
        }

        private void RetourPagePrincipale()
        {
            PagePrincipale.Visibility = Visibility.Visible;
            RetourButton.Visibility = Visibility.Collapsed;
            VisualiseurPhoto.Visibility = Visibility.Collapsed;
            VisualiseurPhoto.ExpanderDetails.IsExpanded = false;
            LeManager.ManagerPhoto.PhotoSelectionne = null;
        }
    }
}
