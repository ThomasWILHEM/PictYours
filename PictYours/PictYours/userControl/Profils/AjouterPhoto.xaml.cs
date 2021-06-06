using BiblioClasse;
using MaterialDesignThemes.Wpf;
using PictYours.Ressources.utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PictYours.userControl.Profils
{
    /// <summary>
    /// Logique d'interaction pour AjouterPhoto.xaml
    /// </summary>
    public partial class AjouterPhoto : UserControl
    {
        /// <summary>
        /// Manager de l'application
        /// </summary>
        public Manager LeManager => (App.Current as App).LeManager;
        
        /// <summary>
        /// Nom du fichier de la photo à ajouter
        /// </summary>
        private string filename;

        /// <summary>
        /// Constructeur du UserControl AjouterPhoto
        /// </summary>
        public AjouterPhoto()
        {
            InitializeComponent();
            MessageSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
        }

        /// <summary>
        /// Affiche le message passé en paramètre dans la Snackbar
        /// </summary>
        /// <param name="message">Message à afficher</param>
        private void AfficherDansSnackbar(string message)
        {
            MessageSnackbar.MessageQueue.Clear();
            MessageSnackbar.MessageQueue.Enqueue(message, null, null, null, false, true);
        }

        /// <summary>
        /// Réinitialise les champs du UserControl
        /// </summary>
        private void ReinitialiserChamps()
        {
            photoAPoster.ImageSource = null;
            CategorieBox.SelectedIndex = 0;
            DescPhoto.Clear();
            LieuPhoto.Clear();
        }

        /// <summary>
        /// Méthode d'évenement appelée lorsque le bouton PosterUnePhoto a été cliqué
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventArgs</param>
        private void PosterButton_Click(object sender, RoutedEventArgs e)
        {
            if (photoAPoster.ImageSource == null)
            {
                AfficherDansSnackbar("Veuillez sélectionnez une photo");
                Debug.WriteLine("L'image est nulle");
                return;
            }
            if (DescPhoto.Text == string.Empty)
            {
                AfficherDansSnackbar("Veuillez saisir une description pour la photo");
                Debug.WriteLine("La description de la photo est nulle");
                return;
            }
            if (LieuPhoto.Text == string.Empty)
            {
                AfficherDansSnackbar("Veuillez saisir un lieu pour la photo");
                Debug.WriteLine("Le lieu de la photo est nul");
                return;
            }
            LeManager.ManagerPhoto.PosterUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, new BiblioClasse.Photo(
                new FileInfo(filename).Extension,
                DescPhoto.Text,
                LieuPhoto.Text,
                LeManager.ManagerUtilisateur.UtilisateurActuel,
                (ECategorie)CategorieBox.SelectedItem)
                );
            //Récupere la photo posté
            Photo photo = LeManager.ManagerUtilisateur.UtilisateurActuel.MesPhotos.First();
            //Enregistre dans le répertoire des images avec son identifiant
            GestionImage.EnregistrerImage(filename, photo.CheminPhoto, GestionImage.TypeEnregistrement.Images,true);
            ReinitialiserChamps();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        /// <summary>
        /// Méthode d'évenement appelée lorsque le bouton Annuler a été cliqué
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventArgs</param>
        private void AnnulerButton_Click(object sender, RoutedEventArgs e)
        {
            ReinitialiserChamps();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        /// <summary>
        /// Méthode d'évenement appelée lorsque le bouton Parcourir a été cliqué
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventArgs</param>
        private void ParcourirButton_Click(object sender, RoutedEventArgs e)
        {
            filename = GestionImage.ChoisirImage();
            if (filename != null)
            {
                photoAPoster.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
            }
        }
    }
}
