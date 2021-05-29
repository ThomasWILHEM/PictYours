using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BiblioClasse
{
    /// <summary>
    /// Définit les attributs communs d'un utilisateur
    /// </summary>
    public abstract class Utilisateur : IEquatable<Utilisateur>,INotifyPropertyChanged
    {
        /// <summary>
        /// Nom de l'utilisateur
        /// </summary>
        public string Nom
        {
            get => nom;
            internal set
            {
                if (value != null)
                {
                    nom = value;
                    OnPropertyChanged();
                }
            }
        }
        private string nom;

        /// <summary>
        /// Pseudo de l'utilisateur
        /// </summary>
        public string Pseudo
        {
            get => pseudo;
            private set
            {
                if (value != null)
                    pseudo = value;
            }
        }
        private string pseudo;

        /// <summary>
        /// Si le booleen est vrai alors l'utilisateur est connecté si non faux
        /// </summary>
        public bool EstConnecte { get; set; }

        /// <summary>
        /// Chemin de la photo de profil de l'utilisateur
        /// </summary>

        public string PhotoDeProfil
        {
            get => photoDeProfil;
            internal set
            {
                if (value != null && value != photoDeProfil)
                {
                    photoDeProfil = value;
                    OnPropertyChanged();
                }
            }
        }
        private string photoDeProfil;

        /// <summary>
        /// Description du profil de l'utilisateur
        /// </summary>
        public string Description
        {
            get => description;
            internal set
            {
                if(value != null && value!= description)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }
        private string description;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Liste des photos de l'utilisateur
        /// </summary>
        protected ObservableCollection<Photo> mesPhotos = new ();
        public ReadOnlyObservableCollection<Photo> MesPhotos { get; }

        /// <summary>
        /// Constructeur d'un utilisateur
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="photoDeProfil">Chemin de la photo de profil</param>
        public Utilisateur(string nom, string pseudo, string photoDeProfil)
        {
            Nom = string.IsNullOrWhiteSpace(nom) ? throw new ArgumentNullException(nameof(nom),"Le nom d'un Utilisateur ne peut pas être nul") : nom;
            Pseudo = string.IsNullOrWhiteSpace(pseudo) ? throw new ArgumentNullException(nameof(pseudo),"Le pseudo d'un Utilisateur ne peut pas être nul") : pseudo;
            PhotoDeProfil = string.IsNullOrWhiteSpace(photoDeProfil) ? throw new ArgumentNullException(nameof(photoDeProfil),"La photo de profil d'un Utilisateur ne peut pas être nulle") : photoDeProfil;
            MesPhotos = new ReadOnlyObservableCollection<Photo>(mesPhotos);
        }

        /// <summary>
        /// Constructeur d'un utilisateur avec une description
        /// </summary>
        /// <param name="nom">Nom de l'utilisateur</param>
        /// <param name="pseudo">Pseudo de l'utilisateur</param>
        /// <param name="photoDeProfil">Chemin de la photo de profil</param>
        /// <param name="description">Description de l'utilisateur</param>
        public Utilisateur(string nom, string pseudo, string photoDeProfil, string description)
            : this(nom, pseudo, photoDeProfil)
        {
            Description = description;
            List<Photo> li = mesPhotos.ToList();
        }

        /// <summary>
        /// Ajoute une photo passee en paramètre dans la liste
        /// de photos de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        public void AjouterPhoto(Photo photo)
        {
            if (photo == null) throw new ArgumentNullException(nameof(photo),"La photo passé passé en paramètre est nul");
            if (MesPhotos.Contains(photo)) throw new InvalidPhotoException($"La photo {photo.Identifiant} à déjà été postée");
            mesPhotos.Insert(0,photo);
        }

    

        /// <summary>
        /// Supprime une photo passee en paramètre de la liste
        /// de photos de l'utilisateur
        /// </summary>
        /// <param name="photo">Photo à supprimer</param>
        public void SupprimerPhoto(string identifiant)
        {
            if (identifiant == null) throw new ArgumentNullException(nameof(identifiant), "L'identifiant passé en paramètre est nul");
            Photo photo = mesPhotos.FirstOrDefault(p => p.Identifiant.Equals(identifiant));
            if (photo == null) throw new InvalidPhotoException($"La photo associé à l'identifiant {identifiant} n'est pas présente dans la liste de photo de l'utilisateur {ToShortString()}");
            mesPhotos.Remove(photo);
        }

        /// <summary>
        /// Permet de voir si l'instance actuel est égale à l'objet en paramètre
        /// selon les attributs choisis
        /// </summary>
        /// <param name="other">Objet à comparer</param>
        public bool Equals(Utilisateur other)
        {
            return Pseudo.Equals(other.Pseudo);
        }

        /// <summary>
        /// Methode virtuelle de Equals
        /// Permet de voir si l'instance actuel est égale à l'objet en paramètre
        /// </summary>
        /// <param name="obj">Objet à comparer</param>
        /// <returns>Renvoie vrai si égale, si non faux</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals((obj as Utilisateur));
        }

        /// <summary>
        /// Permet d'obtenir le HashCode de l'objet actuelle
        /// </summary>
        public override int GetHashCode()
        {
            return Pseudo.GetHashCode();
        }

        /// <summary>
        /// Méthode ToString d'un utilisateur
        /// Permet d'afficher un utilisateur
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Nom}({Pseudo}) Description:{Description}";
        }

        /// <summary>
        /// Permet d'obtenir les informations essentielles d'un Utilisateur
        /// </summary>
        /// <returns>Renvoie la chaine de caractère d'un Utilisateur sous forme réduite</returns>
        public virtual string ToShortString() => $"{Nom}({Pseudo})";
    }
}
