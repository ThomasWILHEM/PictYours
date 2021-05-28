using BiblioClasse;
using PictYours.utils;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace PictYours.userControl
{
    /// <summary>
    /// Logique d'interaction pour Photo.xaml
    /// </summary>
    public partial class Photo : UserControl, INotifyPropertyChanged
    {

        Manager LeManager = (App.Current as App).LeManager;

        public static readonly DependencyProperty UtilisateurProperty
            = DependencyProperty.Register(nameof(BiblioClasse.Photo), typeof(BiblioClasse.Photo), typeof(Photo));

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public BiblioClasse.Photo LaPhoto
        {
            get => GetValue(UtilisateurProperty) as BiblioClasse.Photo;
            set
            {
                SetValue(UtilisateurProperty, value);
                OnPropertyChanged();
            }
        }

        public Photo()
        {
            InitializeComponent();
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

            if ((LeManager.ManagerUtilisateur.UtilisateurActuel as Amateur).PhotosAimees.Contains(LaPhoto))
            {

                LeManager.ManagerPhoto.NePlusAimerUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, LaPhoto);
                JaimeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.StarOutline;
            }
            else
            {
                LeManager.ManagerPhoto.AimerUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, LaPhoto);
                JaimeIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Star;
            }
        }


        public class SupprimerPhotoRequestedEventArgs
        {
        }

        public event EventHandler<SupprimerPhotoRequestedEventArgs> SupprimerPhotoRequested;
        public virtual void OnSupprimerPhotoResqueted() => SupprimerPhotoRequested?.Invoke(this, new SupprimerPhotoRequestedEventArgs());

        private void SupprimerPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeManager.ManagerUtilisateur.UtilisateurActuel.MesPhotos.Contains(LaPhoto))
            {
                LeManager.ManagerPhoto.SupprimerUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, LaPhoto);
                OnSupprimerPhotoResqueted();
            }
        }
    }
}
