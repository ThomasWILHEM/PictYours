using BiblioClasse;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PictYours.userControl.Profils
{
    /// <summary>
    /// Logique d'interaction pour AjouterPhoto.xaml
    /// </summary>
    public partial class AjouterPhoto : UserControl
    {

        public Manager LeManager => (App.Current as App).LeManager;

        public AjouterPhoto()
        {
            InitializeComponent();
        }

        private void PosterButton_Click(object sender, RoutedEventArgs e)
        {
            if (photoAPoster.ImageSource == null) return;
            if (string.IsNullOrEmpty(DescPhoto.Text)) return;
            if (string.IsNullOrEmpty(LieuPhoto.Text)) return;
            LeManager.ManagerPhoto.PosterUnePhoto(LeManager.ManagerUtilisateur.UtilisateurActuel, new BiblioClasse.Photo(photoAPoster.ImageSource.ToString(), DescPhoto.Text, LieuPhoto.Text, LeManager.ManagerUtilisateur.UtilisateurActuel, DateTime.Now, (ECategorie)CategorieBox.SelectedItem));
            ReinitialiserChamps();
            DialogHost.CloseDialogCommand.Execute(null,null);
        }

        private void AnnulerButton_Click(object sender, RoutedEventArgs e)
        {
            ReinitialiserChamps();
        }

        private void ParcourirButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"C:";
            dialog.FileName = "Images";
            dialog.DefaultExt = ".jpg | .png";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                photoAPoster.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
            }
        }

        private void ReinitialiserChamps()
        {
            photoAPoster.ImageSource = null;
            CategorieBox.SelectedIndex = 0;
            DescPhoto.Clear();
            LieuPhoto.Clear();
        }
    }
}
