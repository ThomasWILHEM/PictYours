using AppWpf;
using BiblioClasse;
using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace PictYours.userControl
{
    /// <summary>
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : UserControl
    {
        /// <summary>
        /// Manager de l'application
        /// </summary>
        Manager LeManager = (App.Current as App).LeManager;

        /// <summary>
        /// Constructeur de la page des parametres
        /// </summary>
        public Parametres()
        {
            InitializeComponent();
            MessageSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2.5));
        }

        /// <summary>
        /// Méthode d'évenement appelée lors du clic sur le bouton pour supprimer le compte
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void SupprimerCompteButton_Click(object sender, RoutedEventArgs e)
        {
            bool resultat = LeManager.ManagerUtilisateur.VerifierMotDePasse(MotDePasseBox.Password);
            if (resultat)
            {
                try
                {
                    LeManager.ManagerUtilisateur.SupprimerCompte();
                    new Login().Show();
                    App.Current.Windows[0].Close();
                    Debug.WriteLine("Compte supprimé");
                    DialogHost.CloseDialogCommand.Execute(null,null);
                }
                catch (InvalidUserException exception)
                {
                    Debug.WriteLine($"Exception: {exception.Message}");
                }
            }
            else
            {
                AfficherDansSnackbar("Mot de passe incorrect");
                Debug.WriteLine("Supprimer: Mot de passe incorrect");
            }
        }

        /// <summary>
        /// Réinitialise les paramètres qui sont dans les textBox
        /// </summary>
        internal void ReinitialiserParametres()
        {
            MotDePasseBox.Clear();
            AncienMDPBox.Clear();
            NouveauMDPBox.Clear();
            ConfirmerMDPBox.Clear();
            ConfirmationCheckBox.IsChecked = false;
            tabControl.SelectedIndex = 0;
        }

        /// <summary>
        /// Affiche un message dans la snackbar
        /// </summary>
        /// <param name="message">Message à afficher dans la Snackbar</param>
        private void AfficherDansSnackbar(string message)
        {
            MessageSnackbar.MessageQueue.Clear();
            MessageSnackbar.MessageQueue.Enqueue(message, null,null,null,false,true);
        }

        /// <summary>
        /// Méthode d'évenement appelée lors du clic sur le bouton pour modifier le compte
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            bool resultat = LeManager.ManagerUtilisateur.VerifierMotDePasse(AncienMDPBox.Password);
            if (resultat)
            {
                if(string.IsNullOrWhiteSpace(NouveauMDPBox.Password) && string.IsNullOrWhiteSpace(ConfirmerMDPBox.Password))
                {
                    AfficherDansSnackbar("Le nouveau mot de passe ne peut pas être vide");
                    Debug.WriteLine("Le nouveau mot de passe ne peut pas être vide");
                    return;
                }
                if (NouveauMDPBox.Password.Equals(ConfirmerMDPBox.Password))
                {
                    LeManager.ManagerUtilisateur.ModifierMDP(NouveauMDPBox.Password);
                    Debug.WriteLine("Modifier: Mot de passe modifié");
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    ReinitialiserParametres();
                }
                else
                {
                    AfficherDansSnackbar("Les nouveaux mots de passe ne concordent pas");
                    Debug.WriteLine("Modifier: Les mots de passe ne concordent pas");
                    return;
                }
            }
            else
            {
                AfficherDansSnackbar("Ancien mot de passe incorrect");
                Debug.WriteLine("Modifier: Ancien mot de passe incorrect");
                return;
            }
        }

        /// <summary>
        /// Méthode d'évenement appelée lors du clic sur le bouton pour fermer un dialogHost
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void CloseDialogHostButton_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            ReinitialiserParametres();
        }
    }
}
