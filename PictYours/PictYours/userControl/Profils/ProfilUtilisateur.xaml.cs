using AppWpf;
using BiblioClasse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour ProfilUtilisateur.xaml
    /// </summary>
    public partial class ProfilUtilisateur : UserControl
    {
        public static readonly DependencyProperty UtilisateurProperty
            =DependencyProperty.Register(nameof(Utilisateur),typeof(Utilisateur),typeof(ProfilUtilisateur));



        public Utilisateur Utilisateur
        {
            get => GetValue(UtilisateurProperty) as Utilisateur;
            set => SetValue(UtilisateurProperty, value);
        }

        public ProfilUtilisateur()
        {
            InitializeComponent();
            
        }

        private void DeconnexionButton_Click(object sender, RoutedEventArgs e)
        {
            
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

        private void ParcourirButton2_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.InitialDirectory = @"C:";
            dialog.FileName = "Images";
            dialog.DefaultExt = ".jpg | .png";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                photoAModifier.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
            }
        }
    
    }
}
