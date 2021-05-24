using MaterialDesignThemes.Wpf;
using PictYours;
using BiblioClasse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        }

        private void CreateAccount(object sender, RoutedEventArgs e)
        {

            var createAccount = new CreationCompte();
            createAccount.Show();
            Close();
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
                        //utilisateur.EstConnecte = true;
                        //LeManager.ManagerUtilisateur.UtilisateurActuel = utilisateur;
                        //LeManager.ManagerUtilisateur.UtilisateurSelectionne = utilisateur;
                        LeManager.ManagerUtilisateur.SeConnecter(utilisateur);
                        var main = new MainWindow();
                        main.Show();
                        Close();
                    }
                    else
                    {
                        Debug.WriteLine("Mot de passe incorrect");
                        //Affiche un message
                    }
                }
                else
                {
                    Debug.WriteLine("Identifiant incorrect");
                }
            }
            else
            {
                Debug.WriteLine("Saisir les informations");
                //Dire de saisir des infos
            }
        }
    }
}
