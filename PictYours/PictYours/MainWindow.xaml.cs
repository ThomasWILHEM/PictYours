using AppWpf;
using BiblioClasse;
using MaterialDesignThemes.Wpf;
using PictYours.userControl;
using System;
using System.Windows;
using static PictYours.userControl.VisualisateurPhoto;
using static PictYours.userControl.Profils.ProfilUtilisateur;

namespace PictYours
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Manager de l'application
        /// </summary>
        public Manager LeManager => (App.Current as App).LeManager;

        /// <summary>
        /// Constructeur de MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MesLikeButton.Visibility = LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial ? Visibility.Collapsed : Visibility.Visible;

            LeManager.ManagerPhoto.SelectedPhotoChanged += OnPhotoSelectionneChanged;
            PagePrincipale.UCProfil.ModifierProfilResqueted += OnModifierProfilRequested;
            PagePrincipale.UCProfil.AjouterPhotoRequested += OnAjouterPhotoRequested;
            VisualiseurPhoto.SupprimerPhotoRequested += OnSupprimerPhotoRequested;
            MessageSnackBar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));

            DataContext = this;
        }
        
        /// <summary>
        /// Change la visibilité en "Collapsed" de tous les UserControls présents dans la MainWindow
        /// </summary>
        private void AllMainUCCollapsed()
        {
            PagePrincipale.Visibility = Visibility.Collapsed;
            VisualiseurPhoto.Visibility = Visibility.Collapsed;
            PagePhotoAimees.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Change la visibilité en "Collapsed" de tous les UserControls présents dans le DialogHost
        /// </summary>
        private void AllDialogHostUCCollapsed()
        {
            MesParametres.Visibility = Visibility.Collapsed;
            UCModifierProfil.Visibility = Visibility.Collapsed;
            UCAjouterPhoto.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Retourne à la page principale
        /// </summary>
        private void RetourPagePrincipale()
        {
            AllMainUCCollapsed();
            PagePrincipale.Visibility = Visibility.Visible;
            RetourButton.Visibility = Visibility.Collapsed;
            VisualiseurPhoto.Visibility = Visibility.Collapsed;
            VisualiseurPhoto.ExpanderDetails.IsExpanded = false;
            LeManager.ManagerPhoto.PhotoSelectionne = null;
        }

        /// <summary>
        /// Méthode d'évenement appelé lorsqu'une requête d'ajout de photo a été lancée
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void OnAjouterPhotoRequested(object sender, AjouterPhotoRequestedEventArgs e)
        {
            AllDialogHostUCCollapsed();
            UCAjouterPhoto.Visibility = Visibility.Visible;
            MainDialogHost.IsOpen = true;
        }

        /// <summary>
        /// Méthode d'évenement appelé lorsqu'une requête de suppression de photo a été lancée
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void OnSupprimerPhotoRequested(object sender, SupprimerPhotoRequestedEventArgs e)
        {
            RetourPagePrincipale();
        }

        /// <summary>
        /// Méthode d'évenement appelé lorsqu'une requête de modification de profil a été lancée
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void OnModifierProfilRequested(object sender, ModifierProfilResquetedEventArgs e)
        {
            if (e.Utilisateur is Commercial)
            {
                UCModifierProfil.UCCommercial.Visibility = Visibility.Visible;
            }
            else if (e.Utilisateur is Amateur)
            {
                UCModifierProfil.UCAmateur.Visibility = Visibility.Visible;
            }

            AllDialogHostUCCollapsed();
            UCModifierProfil.Visibility = Visibility.Visible;
            MainDialogHost.IsOpen = true;
        }

        /// <summary>
        /// Méthode d'évenement appelé lorsque la photo sélectionnée à changer
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void OnPhotoSelectionneChanged(object sender, ManagerPhoto.SelectedPhotoChangedEventArgs e)
        {
            if (e.Photo == null) return;
            AllMainUCCollapsed();
            VisualiseurPhoto.Visibility = Visibility.Visible;
            RetourButton.Visibility = Visibility.Visible;

            VisualiseurPhoto.LaPhoto = e.Photo;

            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial)
            {
                VisualiseurPhoto.LikeButton.Visibility = Visibility.Collapsed;
                VisualiseurPhoto.MettreEnAvantButton.Visibility = LeManager.ManagerUtilisateur.UtilisateurActuel.MesPhotos.Contains(e.Photo) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                VisualiseurPhoto.LikeButton.Visibility = Visibility.Visible;
                VisualiseurPhoto.MettreEnAvantButton.Visibility = Visibility.Collapsed;
            }

            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur amateur)
            {
                VisualiseurPhoto.JaimeIcon.Kind = amateur.PhotosAimees.Contains(e.Photo) ? PackIconKind.Star : PackIconKind.StarOutline;
            }

            VisualiseurPhoto.SupprimerPhotoButton.Visibility = LeManager.ManagerUtilisateur.UtilisateurActuel.MesPhotos.Contains(e.Photo) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Méthode d'évenement appelé lors du clic sur le bouton Profil
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            AllMainUCCollapsed();
            RetourPagePrincipale();
            LeManager.ManagerUtilisateur.UtilisateurSelectionne = LeManager.ManagerUtilisateur.UtilisateurActuel;
        }

        /// <summary>
        /// Méthode d'évenement appelé lors du clic sur le bouton Mes Likes
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void MesLikeButton_Click(object sender, RoutedEventArgs e)
        {
            AllMainUCCollapsed();
            RetourButton.Visibility = Visibility.Visible;
            PagePhotoAimees.Visibility = Visibility.Visible;
            LeManager.ManagerPhoto.PhotoSelectionne = null;
            PagePhotoAimees.ListeBoxPhotosAimees.SelectedItem = null;
        }

        /// <summary>
        /// Méthode d'évenement appelé lors du clic sur le bouton Paramètres
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void ParametreButton_Click(object sender, RoutedEventArgs e)
        {
            AllDialogHostUCCollapsed();
            MesParametres.Visibility = Visibility.Visible;
            DialogHost.OpenDialogCommand.Execute(null, null);
        }

        /// <summary>
        /// Méthode d'évenement appelé lors du clic sur le bouton Déconnexion
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void DeconnexionButton_Click(object sender, RoutedEventArgs e)
        {
            RetourPagePrincipale();
            LeManager.ManagerUtilisateur.SeDeconnecter();
            new Login().Show();
            Close();
        }

        /// <summary>
        /// Méthode d'évenement appelé lors du clic sur le bouton Retour
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            RetourPagePrincipale();
            PagePhotoAimees.ListeBoxPhotosAimees.SelectedItem = null;
        }

        /// <summary>
        /// Méthode d'évenement appelé lors de la fermeture de la fenêtre
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            LeManager.SauvegardeDonnees();
        }
    }
}
