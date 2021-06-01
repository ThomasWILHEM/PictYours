using BiblioClasse;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PictYours.userControl
{
    /// <summary>
    /// Logique d'interaction pour PagePrincipale.xaml
    /// </summary>
    public partial class PagePrincipale : UserControl 
    {
        /// <summary>
        /// Manager de l'application
        /// </summary>
        public Manager LeManager => (App.Current as App).LeManager;

        /// <summary>
        /// Liste d'Utilisateurs
        /// </summary>
        public ReadOnlyCollection<Utilisateur> ListeUtilisateur { get; private set; }

        /// <summary>
        /// Constructeur de PagePrincipale
        /// </summary>
        public PagePrincipale()
        {
            InitializeComponent();
            DataContext = this;
            ListeUtilisateur = LeManager.ManagerUtilisateur.ListeUtilisateur;
        }

        /// <summary>
        /// Méthode d'évenement appelée lorsque le mot recherché change
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventArgs</param>
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

        /// <summary>
        /// Méthode d'évenement appelée lorsque l'élément sélectionné de la ListBox change
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">SelectionChangedEventArgs</param>
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
            UCProfil.ExpanderProfil.IsExpanded = false;
        }
    }
}
