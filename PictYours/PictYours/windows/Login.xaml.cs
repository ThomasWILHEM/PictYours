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
        public Manager LeManager => (App.Current as App).LeManager;
        private List<Utilisateur> listeUtilisateur;
        public Login()
        {
            InitializeComponent();
            listeUtilisateur = new List<Utilisateur>(LeManager.ManagerUtilisateur.ListeUtilisateur);
            MessageSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2.5));
        }

        private void CreateAccount(object sender, RoutedEventArgs e)
        {

            var createAccount = new CreationCompte();
            createAccount.Show();
            Close();
        }

        private void AfficherDansSnackbar(string message)
        {
            MessageSnackbar.MessageQueue.Clear();
            MessageSnackbar.MessageQueue.Enqueue(message, null, null, null, false, true);
        }

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
