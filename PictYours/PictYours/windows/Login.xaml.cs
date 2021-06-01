using PictYours;
using BiblioClasse;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System;

namespace AppWpf
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Manager de l'application
        /// </summary>
        public Manager LeManager => (App.Current as App).LeManager;

        /// <summary>
        /// Liste des utilisateurs
        /// </summary>
        private List<Utilisateur> listeUtilisateur;

        /// <summary>
        /// Constructeur de la page de connexion
        /// </summary>
        public Login()
        {
            InitializeComponent();
            listeUtilisateur = new List<Utilisateur>(LeManager.ManagerUtilisateur.ListeUtilisateur);
            MessageSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2.5));
        }
        /// <summary>
        /// Méthode d'évenement appelée lors du clic sur le bouton pour créer un compte
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void CreateAccount(object sender, RoutedEventArgs e)
        {

            var createAccount = new CreationCompte();
            createAccount.Show();
            Close();
        }

        /// <summary>
        /// Affiche un message dans la snackbar
        /// </summary>
        /// <param name="message">Message à afficher dans la Snackbar</param>
        private void AfficherDansSnackbar(string message)
        {
            MessageSnackbar.MessageQueue.Clear();
            MessageSnackbar.MessageQueue.Enqueue(message, null, null, null, false, true);
        }

        /// <summary>
        /// Méthode d'évenement appelée lors du clic sur le bouton pour se connecter
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (pseudoBox.Text != string.Empty && mdpBox.Password != string.Empty)
            {
                Utilisateur u = RechercheUtilisateur.RechercheUnUtilisateur(listeUtilisateur, pseudoBox.Text);
                if (u is UtilisateurPrive utilisateur)
                {
                    if (utilisateur.MotDePasse.Equals(mdpBox.Password))
                    {
                        Debug.WriteLine("Connexion réussie");
                        LeManager.ManagerUtilisateur.SeConnecter(utilisateur);
                        new MainWindow().Show();
                        Close();
                    }
                    else
                    {
                        AfficherDansSnackbar("Mot de passe incorrect");
                        Debug.WriteLine("Mot de passe incorrect");
                    }
                }
                else
                {
                    AfficherDansSnackbar("Identifiant incorrect");
                    Debug.WriteLine("Identifiant incorrect");
                }
            }
            else
            {
                AfficherDansSnackbar("Veuillez saisir vos informations");
                Debug.WriteLine("Saisir les informations");
            }
        }
    }
}
