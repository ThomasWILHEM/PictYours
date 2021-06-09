using AppWpf;
using BiblioClasse;
using MaterialDesignThemes.Wpf;
using PictYours.userControl;
using System;
using System.Windows;
using static PictYours.userControl.VisualisateurPhoto;
using static PictYours.userControl.Profils.ProfilUtilisateur;
using System.Windows.Controls;

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
        /// Précédent UserControl principal
        /// </summary>
        private UserControl PreviousMainUC { get; set; }
        /// <summary>
        /// UserControl principal actuel
        /// </summary>
        private UserControl ActualMainUC { get; set; }

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

            ActualMainUC = PagePrincipale;
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
        /// Méthode qui permet de naviguer vers le UserControl passé en paramètre
        /// </summary>
        /// <param name="userControl">UserControl de destination</param>
        private void AllerA(UserControl userControl)
        {
            AllMainUCCollapsed();
            if (userControl == PagePrincipale) AllerPagePrincipale();
            else if (userControl == PagePhotoAimees) AllerPagePhotosAimees();
            else if (userControl == VisualiseurPhoto) AllerVisualiseurPhoto(LeManager.ManagerPhoto.PhotoSelectionne);
            else throw new ArgumentException("Le UserControl passé en paramètre ne peut pas être atteint");
            PreviousMainUC = ActualMainUC;
            ActualMainUC = userControl;
        }

        /// <summary>
        /// Méthode qui permet d'aller à la page principale
        /// </summary>
        private void AllerPagePrincipale()
        {
            PagePrincipale.UCProfil.ExpanderProfil.IsExpanded = false;
            PagePrincipale.RechercheTextBox.Clear();
            RetourButton.Visibility = Visibility.Collapsed;
            LeManager.ManagerPhoto.PhotoSelectionne = null;
            PagePrincipale.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Méthode qui permet d'aller à la page de photos aimées
        /// </summary>
        private void AllerPagePhotosAimees()
        {
            RetourButton.Visibility = Visibility.Visible;
            VisualiseurPhoto.ExpanderDetails.IsExpanded = false;
            LeManager.ManagerPhoto.PhotoSelectionne = null;
            PagePhotoAimees.ListeBoxPhotosAimees.SelectedItem = null;
            PagePhotoAimees.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Méthode qui permet d'aller au visualiseur de photo et d'afficher la photo passé en paramètre
        /// </summary>
        /// <param name="photo">Photo à afficher</param>
        private void AllerVisualiseurPhoto(Photo photo)
        {
            RetourButton.Visibility = Visibility.Visible;
            VisualiseurPhoto.ExpanderDetails.IsExpanded = false;
            VisualiseurPhoto.LaPhoto = photo;

            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial)
            {
                VisualiseurPhoto.LikeButton.Visibility = Visibility.Collapsed;
                VisualiseurPhoto.MettreEnAvantButton.Visibility = LeManager.ManagerUtilisateur.UtilisateurActuel.MesPhotos.Contains(photo) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                VisualiseurPhoto.LikeButton.Visibility = Visibility.Visible;
                VisualiseurPhoto.MettreEnAvantButton.Visibility = Visibility.Collapsed;
            }

            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur amateur)
            {
                VisualiseurPhoto.JaimeIcon.Kind = amateur.PhotosAimees.Contains(photo) ? PackIconKind.Star : PackIconKind.StarOutline;
            }

            VisualiseurPhoto.SupprimerPhotoButton.Visibility = LeManager.ManagerUtilisateur.UtilisateurActuel.MesPhotos.Contains(photo) ? Visibility.Visible : Visibility.Collapsed;
            VisualiseurPhoto.Visibility = Visibility.Visible;
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
            AllerA(PagePrincipale);
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
            AllerA(VisualiseurPhoto);
        }

        /// <summary>
        /// Méthode d'évenement appelé lors du clic sur le bouton Profil
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            AllerA(PagePrincipale);
            LeManager.ManagerUtilisateur.UtilisateurSelectionne = LeManager.ManagerUtilisateur.UtilisateurActuel;
        }

        /// <summary>
        /// Méthode d'évenement appelé lors du clic sur le bouton Mes Likes
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void MesLikeButton_Click(object sender, RoutedEventArgs e)
        {
            AllerA(PagePhotoAimees);
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
            AllerPagePrincipale();
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
            if (PreviousMainUC == VisualiseurPhoto && ActualMainUC == PagePhotoAimees)
            {
                AllerA(PagePrincipale);
                return;
            }
            AllerA(PreviousMainUC);
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
