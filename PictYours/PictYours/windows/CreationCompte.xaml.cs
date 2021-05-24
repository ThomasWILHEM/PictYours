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


        public CreationCompte()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Vide.Visibility = Visibility.Collapsed;
            if (e.AddedItems[0].ToString().Contains("Amateur"))
            {
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
            if (photoProfil.ImageSource?.ToString() == null)
            {
                Debug.WriteLine("L'image est nulle");
                mySnackBar.MessageQueue?.Enqueue("L'image est nulle",null,null,null,false,true,TimeSpan.FromSeconds(2));
                return;
            }
            if (PasswordBox.Password.Equals(PasswordBoxSame.Password))
            {
                try
                {
                    if (ComboBoxType.SelectedIndex == 0)
                    {
                        LeManager.ManagerUtilisateur.CreerUnCompte(new Amateur(FormA.NomProfil.Text, FormA.PrenomProfil.Text, FormA.PseudoProfil.Text, PasswordBox.Password, photoProfil.ImageSource.ToString(), DescriptionBox.Text, FormA.DateDeNaissanceBox.DisplayDate));
                        Debug.WriteLine("Création éffectué");
                        new MainWindow().Show();
                        Close();
                    }
                    else if (ComboBoxType.SelectedIndex == 1)
                    {
                        LeManager.ManagerUtilisateur.CreerUnCompte(new Commercial(FormC.NomBoxC.Text, FormC.PseudoBoxC.Text, PasswordBox.Password, photoProfil.ImageSource.ToString(), FormC.SiteBox.Text, DescriptionBox.Text));
                        Debug.WriteLine("Création éffectué");
                        new MainWindow().Show();
                        Close();
                    }
                    else
                    {
                        mySnackBar.MessageQueue?.Enqueue("Veuillez selectionner un type de profil",null,null,null,false,true,TimeSpan.FromSeconds(2));
                        Debug.WriteLine("Veuillez selectionner un type de profil");
                    }
                    
                }
                catch (InvalidUserException userException)
                {
                    //Problème dans CreerUnCompte
                    
                    Debug.WriteLine(userException.Message);
                }
                catch (ArgumentNullException nullException)
                {
                    //Certains paramètres sont nuls
                    
                    mySnackBar.MessageQueue?.Enqueue(nullException.Message, null,null,null,false,true,TimeSpan.FromSeconds(2));
                    Debug.WriteLine(nullException.Message);
                }
            }
            else
            {
                Debug.WriteLine("Mauvais mot de passe");
            }
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
