using BiblioClasse;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace PictYours.userControl
{
    /// <summary>
    /// Logique d'interaction pour VisualisateurPhoto.xaml
    /// </summary>
    public partial class VisualisateurPhoto : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Manager de l'application
        /// </summary>
        Manager LeManager = (App.Current as App).LeManager;

        /// <summary>
        /// Dependancy Property d'une photo
        /// </summary>
        public static readonly DependencyProperty PhotoProperty
            = DependencyProperty.Register(nameof(Photo), typeof(Photo), typeof(VisualisateurPhoto));

        /// <summary>
        /// Evenement pour notifier à la vue qu'une propriété à changer
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Méthode associé à PropertyChanged
        /// </summary>
        /// <param name="propertyName">Nom de la propriété changée</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Représente la photo actuelle (selectionée)
        /// </summary>
        public Photo LaPhoto
        {
            get => GetValue(PhotoProperty) as Photo;
            set
            {
                SetValue(PhotoProperty, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Constructeur de la page VisualisateurPhoto 
        /// </summary>
        public VisualisateurPhoto()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Méthode d'évenement appelée lors du clic sur le bouton des J'aimes
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

            if ((LeManager.ManagerUtilisateur.UtilisateurActuel as Amateur).PhotosAimees.Contains(LaPhoto))
            {

                LeManager.ManagerPhoto.NePlusAimerUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, LaPhoto);
                tooltipAimerPhoto.Text = "Aimer la photo";
                JaimeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.StarOutline;
            }
            else
            {
                LeManager.ManagerPhoto.AimerUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, LaPhoto);
                tooltipAimerPhoto.Text = "Ne plus aimer la photo";
                JaimeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Star;
            }
        }

        /// <summary>
        /// Classe d'arguments de l'évvenement SupprimerPhotoRequested
        /// </summary>
        public class SupprimerPhotoRequestedEventArgs
        {
        }

        /// <summary>
        /// Evenement SupprimerPhotoRequested
        /// </summary>
        public event EventHandler<SupprimerPhotoRequestedEventArgs> SupprimerPhotoRequested;
        /// <summary>
        /// Méthode d'évenement associée à SupprimerPhotoRequested
        /// </summary>
        public virtual void OnSupprimerPhotoResqueted() => SupprimerPhotoRequested?.Invoke(this, new SupprimerPhotoRequestedEventArgs());

        /// <summary>
        /// Méthode d'évenement appelée lors du clic sur le bouton pour supprimer une photo
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void SupprimerPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeManager.ManagerUtilisateur.UtilisateurActuel.MesPhotos.Contains(LaPhoto))
            {
                LeManager.ManagerPhoto.SupprimerUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, LaPhoto);
                OnSupprimerPhotoResqueted();
            }
        }

        /// <summary>
        /// Méthode d'évenement appelée lors du clic sur le bouton pour mettre en avant une photo
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void MettreEnAvantButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial commercial
                && LeManager.ManagerPhoto.PhotoSelectionne != null)
            {
                commercial.MettreEnAvantUnePhoto(LeManager.ManagerPhoto.PhotoSelectionne);
            }
        }
    }
}
