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

namespace PictYours
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Manager LeManager => (App.Current as App).LeManager;
        public ReadOnlyCollection<Utilisateur> ListeUtilisateur { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
           
            ListeUtilisateur = LeManager.ManagerUtilisateur.ListeUtilisateur;
            DataContext = this;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuDéroulant.Visibility == Visibility.Collapsed)
                MenuDéroulant.Visibility = Visibility.Visible;
            else
                MenuDéroulant.Visibility = Visibility.Collapsed;
        }
    }
}
