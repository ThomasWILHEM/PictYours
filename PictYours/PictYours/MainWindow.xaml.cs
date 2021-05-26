using AppWpf;
using BiblioClasse;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;

namespace PictYours
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Manager LeManager => (App.Current as App).LeManager;


        public MainWindow()
        {
            InitializeComponent();

            LeManager.ManagerPhoto.PhotoSelectionneChanged += PhotoSelectionneChanged;
            MessageSnackBar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));

            DataContext = this;
        }

        private void PhotoSelectionneChanged(object sender, ManagerPhoto.PhotoSelectionneChangedEventArgs e)
        {
            if (e.Photo != null)
            {
                PagePrincipale.Visibility = Visibility.Collapsed;
                VisualiseurPhoto.Visibility = Visibility.Visible;
                RetourButton.Visibility = Visibility.Visible;

                VisualiseurPhoto.LaPhoto = e.Photo;

                if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial)
                {
                    VisualiseurPhoto.LikeButton.IsEnabled = false;
                }
                if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur amateur)
                {
                    if (amateur.PhotosAimees.Contains(e.Photo))
                    {
                        VisualiseurPhoto.JaimeIcon.Kind = PackIconKind.Star;
                    }
                    else
                    {
                        VisualiseurPhoto.JaimeIcon.Kind = PackIconKind.StarOutline;
                    }
                }

                //if ((LeManager.ManagerUtilisateur.UtilisateurActuel as Amateur).MesPhotos.Contains(e.Photo))   |      
                //{                                                                                              | Ne fonctionne pas
                //     VisualiseurPhoto.JaimeIcon.IsEnabled = false;                                             |
                //}                                                                                              |

            }
        }

        private void DeconnexionButton_Click(object sender, RoutedEventArgs e)
        {
            LeManager.ManagerUtilisateur.SeDeconnecter();
            var login = new Login();
            login.Show();
            Close();
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            RetourPagePrincipale();
            LeManager.ManagerUtilisateur.UtilisateurSelectionne = LeManager.ManagerUtilisateur.UtilisateurActuel;
        }

        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            RetourPagePrincipale();
        }

        private void RetourPagePrincipale()
        {
            PagePrincipale.Visibility = Visibility.Visible;
            RetourButton.Visibility = Visibility.Collapsed;
            VisualiseurPhoto.Visibility = Visibility.Collapsed;
            VisualiseurPhoto.ExpanderDetails.IsExpanded = false;
            LeManager.ManagerPhoto.PhotoSelectionne = null;
        }
    }
}
