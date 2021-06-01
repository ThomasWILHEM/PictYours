using BiblioClasse;
using MaterialDesignThemes.Wpf;
using PictYours;
using PictYours.utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AppWpf
{
    /// <summary>
    /// Logique d'interaction pour CreateAccount.xaml
    /// </summary>
    public partial class CreationCompte : Window
    {

        public Manager LeManager => (App.Current as App).LeManager;

        private string filePhotoProfil;
        private string filePhotoProfilName;

        public CreationCompte()
        {
            InitializeComponent();
            MessageSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2.5));
            DataContext = this;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Vide.Visibility = Visibility.Collapsed;
            if (e.AddedItems[0].ToString().Contains("Amateur"))
            {
                FormA.Visibility = Visibility.Visible;
                FormC.Visibility = Visibility.Collapsed;
                
            }
            else if (e.AddedItems[0].ToString().Contains("Commercial"))
            {
                FormC.Visibility = Visibility.Visible;
                FormA.Visibility = Visibility.Collapsed;
            }
        }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new();
            login.Show();
            Close();
        }

        private void InscriptionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifierPhotoDeProfil()) return;
            if (!VerifierMotDePasse()) return;
            if (!VerifierComboBox()) return;
            if (!VerifierChamps()) return;
            if (LeManager.ManagerUtilisateur.VerifierPseudo(FormA.PseudoProfil.Text) ||
                LeManager.ManagerUtilisateur.VerifierPseudo(FormC.PseudoBoxC.Text))
                AfficherDansSnackbar("Un utilisateur avec un pseudo identique existe déjà");


            if (ComboBoxType.SelectedIndex == 0)
            {
                FileInfo fi = new(filePhotoProfil);
                filePhotoProfilName = $"{FormA.PseudoProfil.Text}{fi.Extension}";
                GestionImage.EnregistrerImage(filePhotoProfil, filePhotoProfilName, GestionImage.TypeEnregistrement.Profils, true);
                LeManager.ManagerUtilisateur.CreerUnCompte(new Amateur(FormA.NomProfil.Text, FormA.PrenomProfil.Text, FormA.PseudoProfil.Text, PasswordBox.Password, filePhotoProfilName, DescriptionBox.Text, FormA.DateDeNaissanceBox.DisplayDate));
            }
            else if (ComboBoxType.SelectedIndex == 1)
            {
                FileInfo fi = new(filePhotoProfil);
                filePhotoProfilName = $"{FormC.PseudoBoxC.Text}{fi.Extension}";
                GestionImage.EnregistrerImage(filePhotoProfil, filePhotoProfilName, GestionImage.TypeEnregistrement.Profils, true);
                LeManager.ManagerUtilisateur.CreerUnCompte(new Commercial(FormC.NomBoxC.Text, FormC.PseudoBoxC.Text, PasswordBox.Password, filePhotoProfilName, FormC.SiteBox.Text, DescriptionBox.Text));

            }
            Debug.WriteLine("Création éffectué");
            new MainWindow().Show();
            Close();
        }

        private void AfficherDansSnackbar(string message)
        {
            MessageSnackbar.MessageQueue.Clear();
            MessageSnackbar.MessageQueue.Enqueue(message, null, null, null, false, true);
        }


        private void parcourirButton_Click(object sender, RoutedEventArgs e)
        {
            filePhotoProfil = GestionImage.ChoisirImage();
            if (filePhotoProfil == null) return;
            IconPhoto.Visibility = Visibility.Collapsed;
            photoProfil.ImageSource = new BitmapImage(new Uri(filePhotoProfil, UriKind.Absolute));
        }

        /// <summary>
        /// Vérifie en fonction de l'utilisateur choisi les informations dans les champs
        /// </summary>
        /// <returns>Renvoie vrai si tout les champs sont corrects sinon faux</returns>
        private bool VerifierChamps()
        {
            if (ComboBoxType.SelectedIndex == 0)
            {
                if (FormA.NomProfil.Text == string.Empty)
                {
                    AfficherDansSnackbar("Veuillez saisir votre nom");
                    return false;
                }
                if (FormA.PrenomProfil.Text == string.Empty)
                {
                    AfficherDansSnackbar("Veuillez saisir votre prénom");
                    return false;
                }
                if (FormA.PseudoProfil.Text == string.Empty)
                {
                    AfficherDansSnackbar("Veuillez saisir votre pseudo");
                }
                if (FormA.DateDeNaissanceBox.Text == string.Empty)
                {
                    AfficherDansSnackbar("Veuillez saisir votre date de naissance");
                    return false;
                }
            }
            else if (ComboBoxType.SelectedIndex == 1)
            {
                if (FormC.NomBoxC.Text == string.Empty)
                {
                    AfficherDansSnackbar("Veuillez saisir votre nom");
                    return false;
                }
                if (FormC.PseudoBoxC.Text == string.Empty)
                {
                    AfficherDansSnackbar("Veuillez saisir un pseudo");
                    return false;
                }
                if (FormC.SiteBox.Text == string.Empty)
                {
                    AfficherDansSnackbar("Veuillez saisir votre site internet");
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Vérifie si le mot de passe n'est pas nul ou vide
        /// </summary>
        /// <returns>Renvoie si le mot de passe est bon sinon faux</returns>
        private bool VerifierMotDePasse()
        {
            if (PasswordBox.Password == string.Empty) AfficherDansSnackbar("Veuillez saisir un mot de passe");
            if (!PasswordBox.Password.Equals(PasswordBoxSame.Password))
            {
                AfficherDansSnackbar("Les mots de passe saisies sont différents");
                Debug.WriteLine("Mauvais mot de passe");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifie si la photo de profil est valide
        /// </summary>
        /// <returns>Renvoie vrai si la photo est valide sinon faux</returns>
        private bool VerifierPhotoDeProfil()
        {
            if (string.IsNullOrWhiteSpace(filePhotoProfil))
            {
                AfficherDansSnackbar("Veuillez sélectionnez une photo");
                Debug.WriteLine("L'image est nulle");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifie si la ComboBox est bien séléctionné
        /// </summary>
        /// <returns>Renvoie vrai si elle est séléctionné sinon faux</returns>
        private bool VerifierComboBox()
        {
            if (ComboBoxType.SelectedIndex != 0 && ComboBoxType.SelectedIndex != 1)
            {
                AfficherDansSnackbar("Veuillez selectionner un type de profil");
                Debug.WriteLine("Veuillez selectionner un type de profil");
                return false;
            }
            return true;
        }

    }
}
