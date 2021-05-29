using AppWpf;
using BiblioClasse;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using static PictYours.userControl.Profils.ProfilUtilisateur;

namespace PictYours
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Manager LeManager => (App.Current as App).LeManager;


        public MainWindow()
        {
            InitializeComponent();
            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial)
            {
                LikeButton.Visibility = Visibility.Collapsed;
            }
            else LikeButton.Visibility = Visibility.Visible;

            LeManager.ManagerPhoto.SelectedPhotoChanged += OnPhotoSelectionneChanged;
            PagePrincipale.UCProfil.ModifierProfilResqueted += OnModifierProfilRequested;
            PagePrincipale.UCProfil.AjouterPhotoRequested += OnAjouterPhotoRequested;
            MessageSnackBar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));

            DataContext = this;
        }

        private void AllMainUCCollapsed()
        {
            PagePrincipale.Visibility = Visibility.Collapsed;
            VisualiseurPhoto.Visibility = Visibility.Collapsed;
            PagePhotoAimees.Visibility = Visibility.Collapsed;
        }

        private void AllDialogHostUCCollapsed()
        {
            MesParametres.Visibility = Visibility.Collapsed;
            UCModifierProfil.Visibility = Visibility.Collapsed;
            UCAjouterPhoto.Visibility = Visibility.Collapsed;
        }

        private void OnAjouterPhotoRequested(object sender, AjouterPhotoRequestedEventArgs e)
        {
            AllDialogHostUCCollapsed();
            UCAjouterPhoto.Visibility = Visibility.Visible;
            MainDialogHost.IsOpen = true;
        }

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

        private void OnPhotoSelectionneChanged(object sender, ManagerPhoto.SelectedPhotoChangedEventArgs e)
        {
            if (e.Photo != null)
            {
                AllMainUCCollapsed();
                VisualiseurPhoto.Visibility = Visibility.Visible;
                RetourButton.Visibility = Visibility.Visible;

                VisualiseurPhoto.LaPhoto = e.Photo;

                if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial)
                {
                    VisualiseurPhoto.LikeButton.Visibility = Visibility.Collapsed;
                    VisualiseurPhoto.MettreEnAvantButton.Visibility = Visibility.Collapsed;
                    if (LeManager.ManagerUtilisateur.UtilisateurActuel.MesPhotos.Contains(e.Photo))
                    {
                        VisualiseurPhoto.MettreEnAvantButton.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    VisualiseurPhoto.MettreEnAvantButton.Visibility = Visibility.Collapsed;
                    VisualiseurPhoto.LikeButton.Visibility = Visibility.Visible;
                }

                if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur amateur)
                {
                    if (amateur.PhotosAimees.Contains(e.Photo))
                    {
                        VisualiseurPhoto.JaimeIcon.Kind = PackIconKind.Star;
                    }
                    else
                    {
                        VisualiseurPhoto.JaimeIcon.Kind = PackIconKind.StarOutline;
                    }
                }

                if (!LeManager.ManagerUtilisateur.UtilisateurActuel.MesPhotos.Contains(e.Photo))
                {
                    VisualiseurPhoto.SupprimerPhotoButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    VisualiseurPhoto.SupprimerPhotoButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void DeconnexionButton_Click(object sender, RoutedEventArgs e)
        {
            RetourPagePrincipale();
            LeManager.ManagerUtilisateur.SeDeconnecter();
            var login = new Login();
            login.Show();
            Close();
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            AllMainUCCollapsed();
            RetourButton.Visibility = Visibility.Visible;
            PagePhotoAimees.Visibility = Visibility.Visible;
            LeManager.ManagerPhoto.PhotoSelectionne = null;
            PagePhotoAimees.ListeBoxPhotosAimees.SelectedItem = null;
        }

        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            AllMainUCCollapsed();
            RetourPagePrincipale();
            LeManager.ManagerUtilisateur.UtilisateurSelectionne = LeManager.ManagerUtilisateur.UtilisateurActuel;
        }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            RetourPagePrincipale();
            PagePhotoAimees.ListeBoxPhotosAimees.SelectedItem = null;
        }

        private void RetourPagePrincipale()
        {
            AllMainUCCollapsed();
            PagePrincipale.Visibility = Visibility.Visible;
            RetourButton.Visibility = Visibility.Collapsed;
            VisualiseurPhoto.Visibility = Visibility.Collapsed;
            VisualiseurPhoto.ExpanderDetails.IsExpanded = false;
            LeManager.ManagerPhoto.PhotoSelectionne = null;
        }

        private void VisualiseurPhoto_SupprimerPhotoRequested(object sender, userControl.Photo.SupprimerPhotoRequestedEventArgs e)
        {
            RetourPagePrincipale();
        }

        private void ParametreButton_Click(object sender, RoutedEventArgs e)
        {
            AllDialogHostUCCollapsed();
            MesParametres.Visibility = Visibility.Visible;
            DialogHost.OpenDialogCommand.Execute(null, null);
        }

    }
}
