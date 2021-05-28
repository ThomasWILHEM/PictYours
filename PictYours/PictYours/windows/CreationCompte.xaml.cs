using BiblioClasse;
using MaterialDesignThemes.Wpf;
using PictYours;
using PictYours.userControl.CreationCompte;
using PictYours.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            if (filePhotoProfil == null)
            {
                AfficherDansSnackbar("Veuillez sélectionnez une photo");
                Debug.WriteLine("L'image est nulle");
                return;
            }
            if (!PasswordBox.Password.Equals(PasswordBoxSame.Password))
            {
                AfficherDansSnackbar("Les mots de passe saisies sont différents");
                Debug.WriteLine("Mauvais mot de passe");
                return;
            }
            try
            {
                if (ComboBoxType.SelectedIndex == 0)
                {
                    FileInfo fi = new(filePhotoProfil);
                    filePhotoProfilName = $"{FormA.PseudoProfil.Text}{fi.Extension}";
                    LeManager.ManagerUtilisateur.CreerUnCompte(new Amateur(FormA.NomProfil.Text, FormA.PrenomProfil.Text, FormA.PseudoProfil.Text, PasswordBox.Password, filePhotoProfilName, DescriptionBox.Text, FormA.DateDeNaissanceBox.DisplayDate));
                }
                else if (ComboBoxType.SelectedIndex == 1)
                {
                    FileInfo fi = new(filePhotoProfil);
                    filePhotoProfilName = $"{FormC.PseudoBoxC.Text}{fi.Extension}";
                    LeManager.ManagerUtilisateur.CreerUnCompte(new Commercial(FormC.NomBoxC.Text, FormC.PseudoBoxC.Text, PasswordBox.Password, filePhotoProfilName, FormC.SiteBox.Text, DescriptionBox.Text));

                }
                else
                {
                    AfficherDansSnackbar("Veuillez selectionner un type de profil");
                    Debug.WriteLine("Veuillez selectionner un type de profil");
                    return;
                }
                GestionImage.EnregistrerImage(filePhotoProfil, filePhotoProfilName, GestionImage.TypeEnregistrement.Profil, true);
                Debug.WriteLine("Création éffectué");
                new MainWindow().Show();
                Close();
            }
            catch (InvalidUserException userException)
            {
                //Problème dans CreerUnCompte
                AfficherDansSnackbar("Un utilisateur avec un pseudo identique existe déjà");

                if (FormA.NomProfil.Text == string.Empty || FormC.NomBoxC.Text == string.Empty) AfficherDansSnackbar("Veuillez saisir votre nom");
                if (FormA.NomProfil.Text == string.Empty || FormC.NomBoxC.Text == string.Empty) AfficherDansSnackbar("Veuillez saisir votre nom");
                Debug.WriteLine(userException.Message);
            }
            catch (ArgumentNullException nullException)
            {
                //Certains paramètres sont nuls
                Debug.WriteLine(nullException.Message);

                if (FormA.NomProfil.Text == string.Empty && FormC.NomBoxC.Text == string.Empty)
                {
                    AfficherDansSnackbar("Veuillez saisir votre nom");
                    return;
                }
                if (FormA.PseudoProfil.Text == string.Empty && FormC.PseudoBoxC.Text == string.Empty)
                {
                    AfficherDansSnackbar("Veuillez saisir un pseudo");
                    return;
                }
                if (ComboBoxType.SelectedIndex == 0)
                {
                    if (FormA.PrenomProfil.Text == string.Empty)
                    {
                        AfficherDansSnackbar("Veuillez saisir votre prénom");
                        return;
                    }
                    if (FormA.DateDeNaissanceBox.Text == string.Empty)
                    {
                        AfficherDansSnackbar("Veuillez saisir votre date de naissance");
                        return;
                    }
                }
                else
                {
                    if (FormC.SiteBox.Text == string.Empty)
                    {
                        AfficherDansSnackbar("Veuillez saisir votre site internet");
                        return;
                    }
                }
                if (PasswordBox.Password == string.Empty) AfficherDansSnackbar("Veuillez saisir un mot de passe");
            }

        }

        private void AfficherDansSnackbar(string message)
        {
            MessageSnackbar.MessageQueue.Clear();
            MessageSnackbar.MessageQueue.Enqueue(message, null, null, null, false, true);
        }


        private void parcourirButton_Click(object sender, RoutedEventArgs e)
        {
            filePhotoProfil = GestionImage.ChooseImage();
            if (filePhotoProfil == null) return;
            IconPhoto.Visibility = Visibility.Collapsed;
            photoProfil.ImageSource = new BitmapImage(new Uri(filePhotoProfil, UriKind.Absolute));
        }

    }
}
