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
        public ReadOnlyCollection<Utilisateur> ListeUtilisateur { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            ListeUtilisateur = LeManager.ManagerUtilisateur.ListeUtilisateur;

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
            if (e.AddedItems.Count == 0)
            {
                LeManager.ManagerUtilisateur.UtilisateurSelectionne = LeManager.ManagerUtilisateur.UtilisateurActuel;
            }
            else
            {
                LeManager.ManagerUtilisateur.UtilisateurSelectionne = e.AddedItems[0] as Utilisateur;
            }
        }

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

        private void RechercheTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (NomPrenomRadioButton.IsChecked == true)
            {
                if (!RechercheTextBox.Text.Equals(""))
                {
                    List<Utilisateur> utilisateursFiltres = RechercheUtilisateur.RechercheParNomEtPrenom(ListeUtilisateur.ToList(), RechercheTextBox.Text);
                    ListBoxUtilisateur.ItemsSource = utilisateursFiltres;
                    if (utilisateursFiltres.Count != 0)
                    {
                        ListBoxUtilisateur_SelectionChanged(this, new SelectionChangedEventArgs(Selector.SelectionChangedEvent, new List<Utilisateur>(), new List<Utilisateur>() { utilisateursFiltres.First() }));
                    }
                }
                else
                {
                    ListBoxUtilisateur.ItemsSource = ListeUtilisateur;
                }

            }
            else
            {
                List<Utilisateur> utilisateursFiltres = RechercheUtilisateur.RechercheParPseudo(ListeUtilisateur.ToList(), RechercheTextBox.Text);
                ListBoxUtilisateur.ItemsSource = utilisateursFiltres;
                if (utilisateursFiltres.Count == 0)
                {
                    ListBoxUtilisateur.SelectedItem = LeManager.ManagerUtilisateur.UtilisateurActuel;
                    //ListBoxUtilisateur_SelectionChanged(this, new SelectionChangedEventArgs(Selector.SelectionChangedEvent, new List<Utilisateur>(), new List<Utilisateur>()));
                }
                else
                {
                    ListBoxUtilisateur.SelectedItem = utilisateursFiltres.First();
                    //ListBoxUtilisateur_SelectionChanged(this, new SelectionChangedEventArgs(Selector.SelectionChangedEvent, new List<Utilisateur>(), new List<Utilisateur>() { utilisateursFiltres.First() }));
                }

            }
        }
    }
}
