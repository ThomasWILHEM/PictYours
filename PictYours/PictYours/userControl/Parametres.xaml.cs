using AppWpf;
using BiblioClasse;
using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PictYours.userControl
{
    /// <summary>
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : UserControl
    {
        Manager LeManager = (App.Current as App).LeManager;

        public Parametres()
        {
            InitializeComponent();
            MessageSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2.5));
        }

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

        internal void ReinitialiserParametres()
        {
            MotDePasseBox.Clear();
            AncienMDPBox.Clear();
            NouveauMDPBox.Clear();
            ConfirmerMDPBox.Clear();
            ConfirmationCheckBox.IsChecked = false;
            tabControl.SelectedIndex = 0;
        }

        private void AfficherDansSnackbar(string message)
        {
            MessageSnackbar.MessageQueue.Clear();
            MessageSnackbar.MessageQueue.Enqueue(message, null,null,null,false,true);
        }

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

        private void CloseDialogHostButton_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            ReinitialiserParametres();
        }
    }
}
