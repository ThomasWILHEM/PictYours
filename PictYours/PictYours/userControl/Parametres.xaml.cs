using AppWpf;
using BiblioClasse;
using MaterialDesignThemes.Wpf;
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
        Manager LeManager = (App.Current as App).LeManager;

        public Parametres()
        {
            InitializeComponent();
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
                }
                catch (InvalidUserException exception)
                {
                    Debug.WriteLine($"Exception: {exception.Message}");
                }
            }
            else
            {
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

        private void ModifierButton_Click(object sender, RoutedEventArgs e)
        {
            bool resultat = LeManager.ManagerUtilisateur.VerifierMotDePasse(AncienMDPBox.Password);
            if (resultat)
            {
                if (NouveauMDPBox.Password.Equals(ConfirmerMDPBox.Password))
                {
                    LeManager.ManagerUtilisateur.ModifierMDP(NouveauMDPBox.Password);
                    Debug.WriteLine("Modifier: Mot de passe modifié");
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else
                {
                    Debug.WriteLine("Modifier: Les mots de passe ne concordent pas");
                }
            }
            else
            {
                Debug.WriteLine("Modifier: Ancien mot de passe incorrect");
            }
            ReinitialiserParametres();
        }
    }
}
