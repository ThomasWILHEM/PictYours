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
        public Manager LeManager => (App.Current as App).LeManager;

        public static readonly DependencyProperty UtilisateurProperty
            = DependencyProperty.Register(nameof(Utilisateur), typeof(Utilisateur), typeof(ProfilUtilisateur));



        public Utilisateur Utilisateur
        {
            get => GetValue(UtilisateurProperty) as Utilisateur;
            set => SetValue(UtilisateurProperty, value);
        }

        public ProfilUtilisateur()
        {
            InitializeComponent();
        }

        private void ModifButton_Click(object sender, RoutedEventArgs e)
        {
            OnModifierProfilResqueted(LeManager.ManagerUtilisateur.UtilisateurActuel);
        }

        //-------------------------
        //Déclaration d'un évenement ModifierProfilRequested avec les EventArgs associés pour signaler lorsque le bouton modifier est cliqué
        public class ModifierProfilResquetedEventArgs
        {
            public Utilisateur Utilisateur { get; private set; }
            public ModifierProfilResquetedEventArgs(Utilisateur utilisateur) => Utilisateur = utilisateur;
        }
        public event EventHandler<ModifierProfilResquetedEventArgs> ModifierProfilResqueted;
        public virtual void OnModifierProfilResqueted(Utilisateur utilisateur) => ModifierProfilResqueted?.Invoke(this, new ModifierProfilResquetedEventArgs(utilisateur));

        //-------------------------
        //Déclaration d'un évenement ModifierProfilRequested avec les EventArgs associés pour signaler lorsque le bouton modifier est cliqué
        public class AjouterPhotoRequestedEventArgs
        {
        }

        public event EventHandler<AjouterPhotoRequestedEventArgs> AjouterPhotoRequested;
        public virtual void OnAjouterPhotoResqueted() => AjouterPhotoRequested?.Invoke(this, new AjouterPhotoRequestedEventArgs());

        private void AjouterPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OnAjouterPhotoResqueted();
        }
    }
}
