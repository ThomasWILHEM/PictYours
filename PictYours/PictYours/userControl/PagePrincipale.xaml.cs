using BiblioClasse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Logique d'interaction pour PagePrincipale.xaml
    /// </summary>
    public partial class PagePrincipale : UserControl 
    {
        public PagePrincipale()
        {
            InitializeComponent();
            DataContext = this;
            ListeUtilisateur = LeManager.ManagerUtilisateur.ListeUtilisateur;
        }

        public Manager LeManager => (App.Current as App).LeManager;

        public ReadOnlyCollection<Utilisateur> ListeUtilisateur { get; private set; }

        
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
                if (utilisateursFiltres.Count > 0)
                {
                    ListBoxUtilisateur.SelectedItem = utilisateursFiltres.First();
                }


            }
        }

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
    }
}
