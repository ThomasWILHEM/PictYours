using AppWpf;
using BiblioClasse;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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

            

            DataContext = this;
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
            LeManager.ManagerUtilisateur.UtilisateurSelectionne = LeManager.ManagerUtilisateur.UtilisateurActuel;
        }

        private void CloseDialogHostButton_Click(object sender, RoutedEventArgs e)
        {
            MesParametres.ReinitialiserParametres();
        }

        
    }
}
