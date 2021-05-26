using BiblioClasse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public ModifierProfil()
        {
            InitializeComponent();
            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur)
            {
                UCAmateur.NomBox.DataContext = this;
            }
            else if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial commercial)
            {
                UCCommercial.NomBox.DataContext = this;
                UCCommercial.SiteBox.Text = commercial.SiteWeb;
            }
            Nom = LeManager.ManagerUtilisateur.UtilisateurActuel.Nom;
            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur amateur)
            {
                UCAmateur.PrenomBox.DataContext = this;
                Prenom = amateur.Prenom;
                UCAmateur.DateDeNaissanceBox.DataContext = this;
                DateDeNaissance = amateur.DateDeNaissance;
            }
            DescBox.DataContext = this;
            Description = LeManager.ManagerUtilisateur.UtilisateurActuel.Description;
        }

        private void ParcourirPhotoAModifierButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"C:";
            dialog.FileName = "Images";
            //dialog.Filter = "*.jpg | *.png";
            dialog.DefaultExt = ".jpg | .png";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                photoAModifier.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
            }
        }

        public string Nom { get; set; }
        public string Prenom { get; set; }

        public string SiteWeb { get; set; }
        public string Description { get; set; }
        public DateTime DateDeNaissance { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void EnregistrerButton_Click(object sender, RoutedEventArgs e)
        {
            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur)
            {
                LeManager.ManagerUtilisateur.ModifierPrenom(UCAmateur.PrenomBox.Text);

                LeManager.ManagerUtilisateur.ModifierDateDeNaissance(UCAmateur.DateDeNaissanceBox.DisplayDate);
                LeManager.ManagerUtilisateur.ModifierNom(UCAmateur.NomBox.Text);
            }
            else if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial)
            {
                LeManager.ManagerUtilisateur.ModifierNom(UCCommercial.NomBox.Text);
                LeManager.ManagerUtilisateur.ModifierSiteWeb(UCCommercial.SiteBox.Text);
            }
            LeManager.ManagerUtilisateur.ModifierDescription(DescBox.Text);
            LeManager.ManagerUtilisateur.ModifierPhotoDeProfil(photoAModifier.ImageSource.ToString());
        }


    }
}
