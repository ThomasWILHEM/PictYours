using BiblioClasse;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PictYours.userControl.Profils
{

    /// <summary>
    /// Logique d'interaction pour ProfilUtilisateur.xaml
    /// </summary>
    public partial class ProfilUtilisateur : UserControl
    {
        /// <summary>
        /// Manager de l'application
        /// </summary>
        public Manager LeManager => (App.Current as App).LeManager;

        /// <summary>
        /// Dependancy Property d'un Utilisateur
        /// </summary>
        public static readonly DependencyProperty UtilisateurProperty = DependencyProperty.Register(nameof(Utilisateur), typeof(Utilisateur), typeof(ProfilUtilisateur));

        /// <summary>
        /// Utilisateur du UserControl ProfilUtilisateur
        /// </summary>
        public Utilisateur Utilisateur
        {
            get => GetValue(UtilisateurProperty) as Utilisateur;
            set => SetValue(UtilisateurProperty, value);
        }

        /// <summary>
        /// Constructeur de ProfilUtilisateur
        /// </summary>
        public ProfilUtilisateur()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Méthode d'évenement appelée lorsque le bouton "Modifer" est cliqué
        /// Lance l'évenement OnModifierProfilRequested
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventArgs</param>
        private void ModifButton_Click(object sender, RoutedEventArgs e)
        {
            OnModifierProfilResqueted(LeManager.ManagerUtilisateur.UtilisateurActuel);
        }

        /// <summary>
        /// Méthode d'évenement appelée lorsque le bouton "+"(Ajouter) est cliqué
        /// Lance l'évenement OnAjouterPhotoRequested
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventArgs</param>
        private void AjouterPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OnAjouterPhotoResqueted();
        }

        //-------------------------
        //-----
        //Déclaration d'un évenement ModifierProfilRequested avec les EventArgs associés pour signaler lorsque le bouton modifier est cliqué
        //-----
        /// <summary>
        /// Classe d'arguments de l'évenement ModifierProfilRequested
        /// </summary>
        public class ModifierProfilResquetedEventArgs
        {
            public Utilisateur Utilisateur { get; private set; }
            public ModifierProfilResquetedEventArgs(Utilisateur utilisateur) => Utilisateur = utilisateur;
        }
        /// <summary>
        /// Evenement ModifierProfilRequested
        /// </summary>
        public event EventHandler<ModifierProfilResquetedEventArgs> ModifierProfilResqueted;
        /// <summary>
        /// Méthode d'évenement associé à ModifierProfilRequested
        /// </summary>
        /// <param name="utilisateur">Utilisateur qui a lancé l'évenement</param>
        public virtual void OnModifierProfilResqueted(Utilisateur utilisateur) => ModifierProfilResqueted?.Invoke(this, new ModifierProfilResquetedEventArgs(utilisateur));
        //-------------------------

        //-------------------------
        //-----
        //Déclaration d'un évenement ModifierProfilRequested avec les EventArgs associés pour signaler lorsque le bouton modifier est cliqué
        //-----
        /// <summary>
        /// Classe d'arguments de l'évenement AjouterPhotoRequested
        /// </summary>
        public class AjouterPhotoRequestedEventArgs { }
        /// <summary>
        /// Evenement AjouterPhotoRequested
        /// </summary>
        public event EventHandler<AjouterPhotoRequestedEventArgs> AjouterPhotoRequested;
        /// <summary>
        /// Méthode d'évenement associé ) AjouterPhotoRequested
        /// </summary>
        public virtual void OnAjouterPhotoResqueted() => AjouterPhotoRequested?.Invoke(this, new AjouterPhotoRequestedEventArgs());
        //-------------------------
    }
}
