using BiblioClasse;
using MaterialDesignThemes.Wpf;
using PictYours.utils;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PictYours.userControl.Profils
{
    /// <summary>
    /// Logique d'interaction pour ModifierProfil.xaml
    /// </summary>
    public partial class ModifierProfil : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Manager de l'application
        /// </summary>
        public Manager LeManager => (App.Current as App).LeManager;

        /// <summary>
        /// Propriété temporaire du nom avant la sauvegarde
        /// </summary>
        public string Nom
        {
            get => nom;
            set
            {
                if (value != null && value != nom)
                {
                    nom = value;
                    OnPropertyChanged();
                }
            }
        }
        private string nom;

        /// <summary>
        /// Propriété temporaire de la description avant la sauvegarde
        /// </summary>
        public string Description
        {
            get => description;
            set
            {
                if (value != null && value != description)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }
        private string description;

        /// <summary>
        /// Propriété temporaire pour l'emplacement de la photo avant la sauvegarde
        /// </summary>
        public string CheminPhoto
        {
            get => cheminPhoto;
            set
            {
                if (value != null && value != cheminPhoto)
                {
                    cheminPhoto = value;
                    OnPropertyChanged();
                }
            }
        }
        private string cheminPhoto;

        /// <summary>
        /// Propriété temporaire du prénom avant la sauvegarde
        /// </summary>
        public string Prenom
        {
            get => prenom;
            set
            {
                if (value != null && value != prenom)
                {
                    prenom = value;
                    OnPropertyChanged();
                }
            }
        }
        private string prenom;

        /// <summary>
        /// Propriété temporaire pour la date de naissance avant la sauvegarde
        /// </summary>
        public DateTime? DateDeNaissance
        {
            get => dateDeNaissance;
            set
            {
                if (value != dateDeNaissance)
                {
                    dateDeNaissance = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime? dateDeNaissance;

        /// <summary>
        /// Propriété temporaire pour le site web avant la sauvegarde
        /// </summary>
        public string SiteWeb
        {
            get => siteWeb;
            set
            {
                if (value != null && value != siteWeb)
                {
                    siteWeb = value;
                    OnPropertyChanged();
                }
            }
        }
        private string siteWeb;

        /// <summary>
        /// Evenement pour notifier à la vue qu'une propriété à changer
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Méthode associé à PropertyChanged
        /// </summary>
        /// <param name="propertyName">Nom de la propriété changée</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
       

        /// <summary>
        /// Constructeur du UserControl ModifierProfil
        /// </summary>
        public ModifierProfil()
        {
            InitializeComponent();
            DataContext = this;
            MessageSnackbar.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
            InitialiserChamps();
        }

        /// <summary>
        /// Méthode qui permet d'initialiser tous les champs du formulaire de modification
        /// </summary>
        private void InitialiserChamps()
        {
            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur amateur)
            {
                Prenom = amateur.Prenom;
                DateDeNaissance = amateur.DateDeNaissance;
            }
            else if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial commercial)
            {
                SiteWeb = commercial.SiteWeb;
            }
            Nom = LeManager.ManagerUtilisateur.UtilisateurActuel.Nom;
            Description = LeManager.ManagerUtilisateur.UtilisateurActuel.Description;
            CheminPhoto = LeManager.ManagerUtilisateur.UtilisateurActuel.PhotoDeProfil;
            PhotoAModifier.ImageSource = new BitmapImage(new Uri(GestionImage.ProfilsPath + "\\" + CheminPhoto, UriKind.Relative));
        }

        /// <summary>
        /// Permet d'afficher le message en paramètre dans la Snackbar de ModifierProfil
        /// </summary>
        /// <param name="message">Message à afficher</param>
        private void AfficherDansSnackbar(string message)
        {
            MessageSnackbar.MessageQueue.Clear();
            MessageSnackbar.MessageQueue.Enqueue(message, null, null, null, false, true);
        }

        /// <summary>
        /// Vérifie les champs lors de la modification du profil
        /// </summary>
        /// <returns>Renvoie vrai si tous les champs sont corrects sinon faux</returns>
        private bool VerifierChamps()
        {
            if (string.IsNullOrWhiteSpace(Nom))
            {
                AfficherDansSnackbar("Le nom ne peut pas être vide");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Prenom))
            {
                AfficherDansSnackbar("Le prénom ne peut pas être vide");
                return false;
            }
            if(DateDeNaissance == null)
            {
                AfficherDansSnackbar("La date ne peut pas être vide");
                return false;
            }
            if (string.IsNullOrWhiteSpace(CheminPhoto))
            {
                AfficherDansSnackbar("Le photo ne peut pas être vide");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Permet de choisir une nouvelle photo pour le profil
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void ParcourirPhotoAModifierButton_Click(object sender, RoutedEventArgs e)
        {
            string filename = GestionImage.ChoisirImage();
            if (filename == null) return;
            PhotoAModifier.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
            CheminPhoto = filename;
        }

        /// <summary>
        /// Méthode qui permet d'enregistrer les nouvelles informations si elles sont valides
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventArgs</param>
        private void EnregistrerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!VerifierChamps()) return;

            if (LeManager.ManagerUtilisateur.UtilisateurActuel is Amateur)
            {
                LeManager.ManagerUtilisateur.ModifierPrenom(Prenom);

                LeManager.ManagerUtilisateur.ModifierDateDeNaissance(DateDeNaissance.Value);
                LeManager.ManagerUtilisateur.ModifierNom(Nom);
            }
            else if (LeManager.ManagerUtilisateur.UtilisateurActuel is Commercial)
            {
                LeManager.ManagerUtilisateur.ModifierNom(Nom);
                LeManager.ManagerUtilisateur.ModifierSiteWeb(SiteWeb);
            }
            LeManager.ManagerUtilisateur.ModifierDescription(Description);

            if (CheminPhoto != LeManager.ManagerUtilisateur.UtilisateurActuel.PhotoDeProfil)
            {
                string cheminFinal = GestionImage.EnregistrerImage(
                                CheminPhoto,
                                LeManager.ManagerUtilisateur.UtilisateurActuel.Pseudo,
                                GestionImage.TypeEnregistrement.Profils,
                                false
                                );
                FileInfo fi = new(cheminFinal);
                Debug.WriteLine(fi.Name);
                LeManager.ManagerUtilisateur.ModifierPhotoDeProfil(fi.Name);
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        /// <summary>
        /// Méthode qui est déclenché lors du clique sur le bouton "Retour"
        /// </summary>
        /// <param name="sender">sender de l'évenement</param>
        /// <param name="e">RoutedEventAgrs</param>
        private void RetourButton_Click(object sender, RoutedEventArgs e)
        {
            InitialiserChamps();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
