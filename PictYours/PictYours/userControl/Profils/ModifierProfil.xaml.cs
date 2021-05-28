using BiblioClasse;
using MaterialDesignThemes.Wpf;
using PictYours.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PictYours.userControl.Profils
{
    /// <summary>
    /// Logique d'interaction pour ModifierProfil.xaml
    /// </summary>
    public partial class ModifierProfil : UserControl, INotifyPropertyChanged
    {
        public Manager LeManager => (App.Current as App).LeManager;

        public string Nom
        {
            get => nom;
            set
            {
                if (value != null && value != nom)
                {
                    nom = value;
                    OnPropertyChanged();
                }
            }
        }
        private string nom;

        public string Description
        {
            get => description;
            set
            {
                if (value != null && value != description)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }
        private string description;

        public string CheminPhoto
        {
            get => cheminPhoto;
            set
            {
                if (value != null && value != cheminPhoto)
                {
                    cheminPhoto = value;
                    OnPropertyChanged();
                }
            }
        }
        private string cheminPhoto;

        public string Prenom
        {
            get => prenom;
            set
            {
                if (value != null && value != prenom)
                {
                    prenom = value;
                    OnPropertyChanged();
                }
            }
        }
        private string prenom;

        public DateTime DateDeNaissance
        {
            get => dateDeNaissance;
            set
            {
                if (value != dateDeNaissance)
                {
                    dateDeNaissance = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime dateDeNaissance;

        public string SiteWeb
        {
            get => siteWeb;
            set
            {
                if (value != null && value != siteWeb)
                {
                    siteWeb = value;
                    OnPropertyChanged();
                }
            }
        }
        private string siteWeb;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string parameterName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(parameterName));

        public ModifierProfil()
        {
            InitializeComponent();
            DataContext = this;

            InitialiserChamps();
        }

        private void InitialiserChamps()
        {
            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur amateur)
            {
                Prenom = amateur.Prenom;
                DateDeNaissance = amateur.DateDeNaissance;
            }
            else if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial commercial)
            {
                SiteWeb = commercial.SiteWeb;
            }
            Nom = LeManager.ManagerUtilisateur.UtilisateurActuel.Nom;
            Description = LeManager.ManagerUtilisateur.UtilisateurActuel.Description;
            CheminPhoto = LeManager.ManagerUtilisateur.UtilisateurActuel.PhotoDeProfil;
            PhotoAModifier.ImageSource = new BitmapImage(new Uri(GestionImage.ProfilPath + "\\" + CheminPhoto, UriKind.Relative));
        }

        private void ParcourirPhotoAModifierButton_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            //dialog.InitialDirectory = @"C:";
            //dialog.FileName = "Images";
            ////dialog.Filter = "*.jpg | *.png";
            //dialog.DefaultExt = ".jpg | .png";

            //bool? result = dialog.ShowDialog();

            //if (result == true)
            //{
            //    string filename = dialog.FileName;
            //    PhotoAModifier.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
            //}

            string filename = GestionImage.ChooseImage();
            if (filename == null) return;
            PhotoAModifier.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
            CheminPhoto = filename;
        }

        private void EnregistrerButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur)
            {
                LeManager.ManagerUtilisateur.ModifierPrenom(Prenom);

                LeManager.ManagerUtilisateur.ModifierDateDeNaissance(DateDeNaissance);
                LeManager.ManagerUtilisateur.ModifierNom(Nom);
            }
            else if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial)
            {
                LeManager.ManagerUtilisateur.ModifierNom(Nom);
                LeManager.ManagerUtilisateur.ModifierSiteWeb(SiteWeb);
            }
            LeManager.ManagerUtilisateur.ModifierDescription(Description);

            if (CheminPhoto != LeManager.ManagerUtilisateur.UtilisateurActuel.PhotoDeProfil)
            {
                string cheminFinal = GestionImage.EnregistrerImage(
                                CheminPhoto,
                                LeManager.ManagerUtilisateur.UtilisateurActuel.Pseudo,
                                GestionImage.TypeEnregistrement.Profil,
                                false
                                );
                FileInfo fi = new(cheminFinal);
                Debug.WriteLine(fi.Name);
                LeManager.ManagerUtilisateur.ModifierPhotoDeProfil(fi.Name);
            }
        }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            InitialiserChamps();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
