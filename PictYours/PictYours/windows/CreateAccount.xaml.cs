using PictYours;
using PictYours.userControl.CreateAccount;
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
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
            Vide.Visibility = Visibility.Visible;
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

        
    }
}
