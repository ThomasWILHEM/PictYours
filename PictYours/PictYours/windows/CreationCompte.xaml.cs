using BiblioClasse;
using PictYours;
using PictYours.userControl.CreationCompte;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public Commercial LUtilisateur { get; set; }

        public CreationCompte()
        {
            InitializeComponent();
            Vide.Visibility = Visibility.Visible;
            var c = new Commercial("g", "g", "g", "g", "g", "g");
            LUtilisateur = new Commercial(c.Nom, c.Pseudo, c.MotDePasse, c.PhotoDeProfil, c.SiteWeb, c.Description);
            DataContext = this;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Vide.Visibility = Visibility.Collapsed;
            if (e.AddedItems[0].ToString().Contains("Amateur")) {
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
            MainWindow main = new();
            main.Show();
            Close();
        }

        private void parcourirButton_Click(object sender, RoutedEventArgs e)
        {
            IconPhoto.Visibility = Visibility.Collapsed;
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"C:";
            dialog.FileName = "Images";
            dialog.DefaultExt = ".jpg | .png";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                photoProfil.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
            }
        }

    }
}
